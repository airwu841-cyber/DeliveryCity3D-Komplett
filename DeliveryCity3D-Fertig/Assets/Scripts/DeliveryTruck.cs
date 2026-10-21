using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DeliveryTruck : MonoBehaviour
{
    public string truckName = "Lieferwagen";
    public Transform parcelVisual;

    // Der Wagen erledigt immer dieselben sechs Schritte in dieser Reihenfolge.
    enum State { Idle, ToDepot, Loading, ToHouse, Unloading, Returning }
    private State state;
    private NavMeshAgent agent;
    private DeliveryStop depot;
    private DeliveryGame game;
    private float timer;
    private Vector3 parkPosition;
    private int drivingPriority;

    public bool IsBusy => state != State.Idle;
    public DeliveryStop AssignedTarget { get; private set; }
    public string Status { get; private set; } = "Bereit";

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        drivingPriority = agent.avoidancePriority;
        parkPosition = transform.position;
        ShowParcel(false);
    }

    public bool TryDeliver(DeliveryStop depot, DeliveryStop target, DeliveryGame game)
    {
        if (IsBusy || AssignedTarget != null) return false;
        if (depot == null || target == null || game == null || !depot.isDepot || target.isDepot)
        {
            Fail("Lager oder Haus fehlt.");
            return false;
        }
        this.depot = depot;
        this.game = game;
        AssignedTarget = target;
        if (DriveTo(depot.Position, State.ToDepot, "Fährt zum Lager")) return true;
        ClearAssignment(); // Ohne Fahrt bleibt der Auftrag in der offenen Liste.
        return false;
    }

    void Update()
    {
        if (!IsBusy) return;
        if (!AgentReady() || (state != State.Returning && (depot == null || AssignedTarget == null)))
        {
            Fail("Fahrt abgebrochen: Fahrzeug oder Ziel fehlt.");
            return;
        }
        timer += Time.deltaTime;

        // Beim Laden und Entladen bleibt der Wagen kurz stehen.
        if (state == State.Loading)
        {
            if (timer < 0.8f) return;
            ShowParcel(true);
            DriveTo(AssignedTarget.Position, State.ToHouse, "Liefert an " + AssignedTarget.stopName);
            return;
        }
        if (state == State.Unloading)
        {
            if (timer < 0.8f) return;
            ShowParcel(false);
            DeliveryStop deliveredStop = AssignedTarget;
            ClearAssignment();
            game.OnDelivered(deliveredStop);
            DriveTo(parkPosition, State.Returning, "Rückfahrt zum Parkplatz");
            return;
        }

        // Ein fehlerhafter oder blockierter Weg darf keinen Auftrag festhalten.
        if (timer > 60f)
        {
            Fail("Fahrt abgebrochen: Zeitlimit erreicht.");
            return;
        }
        if (agent.pathPending) return;
        if (agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            Fail("Ziel ist über die Straßen nicht erreichbar.");
            return;
        }
        if (!agent.hasPath && Vector3.Distance(agent.nextPosition, agent.destination)
            > agent.stoppingDistance + 0.15f)
        {
            Fail("Fahrt abgebrochen: Fahrweg fehlt.");
            return;
        }
        if (agent.remainingDistance > agent.stoppingDistance + 0.15f) return;
        if (agent.velocity.sqrMagnitude > 0.1f) return;

        agent.isStopped = true;
        timer = 0f;
        if (state == State.ToDepot)
        {
            state = State.Loading;
            Status = "Paket wird geladen";
        }
        else if (state == State.ToHouse)
        {
            state = State.Unloading;
            Status = "Paket wird zugestellt";
        }
        else if (state == State.Returning)
        {
            state = State.Idle;
            agent.avoidancePriority = 0;
            Status = "Bereit";
        }
    }

    bool DriveTo(Vector3 position, State nextState, string status)
    {
        if (!AgentReady())
        {
            Fail("Fahrzeug steht nicht auf einer Straße.");
            return false;
        }
        NavMeshPath path = new NavMeshPath();
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 2f, agent.areaMask)
            || !agent.CalculatePath(hit.position, path)
            || path.status != NavMeshPathStatus.PathComplete || !agent.SetPath(path))
        {
            Fail("Ziel ist über die Straßen nicht erreichbar.");
            return false;
        }
        agent.avoidancePriority = drivingPriority;
        agent.isStopped = false;
        state = nextState;
        timer = 0f;
        Status = status;
        return true;
    }

    public void ResetTruck(Vector3 position)
    {
        Fail("Bereit");
        ClearAssignment();
        parkPosition = position;
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (agent != null && gameObject.activeInHierarchy
            && NavMesh.SamplePosition(position, out NavMeshHit hit, 3f, agent.areaMask))
        {
            // Erst im Start aktivieren: Dann ist das NavMesh der Stadt registriert.
            if (!agent.enabled) transform.position = hit.position;
            agent.enabled = true;
            if (agent.Warp(hit.position))
            {
                parkPosition = hit.position;
                agent.avoidancePriority = 0; // Fahrende Wagen weichen geparkten Wagen aus.
                agent.isStopped = true;
                return;
            }
        }
        Status = "Startposition liegt nicht auf einer Straße.";
    }

    bool AgentReady() => agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh;

    public void ClearAssignment()
    {
        AssignedTarget = null;
    }

    void ShowParcel(bool visible)
    {
        if (parcelVisual != null) parcelVisual.gameObject.SetActive(visible);
    }

    void Fail(string message)
    {
        if (AgentReady())
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
        if (agent != null) agent.avoidancePriority = 0;
        state = State.Idle;
        // Das Spiel stellt AssignedTarget in der nächsten Update-Runde zurück.
        ShowParcel(false);
        Status = message;
    }
}
