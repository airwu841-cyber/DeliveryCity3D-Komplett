using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Person 2: Spielregeln, Aufträge und Eingaben. Die Fahrt übernimmt DeliveryTruck.
public class DeliveryGame : MonoBehaviour
{
    public int cityNumber = 1;
    public string cityName = "Little Oak";
    public int deliveryGoal = 6;
    public float shiftDuration = 240f;
    public DeliveryStop depot;
    public DeliveryStop[] destinations;
    public DeliveryTruck[] trucks;
    public Transform[] parkingSpots;
    public AudioSource audioSource;
    public AudioClip selectSound;
    public AudioClip deliverySound;

    public bool Running { get; private set; }
    public bool Paused { get; private set; }
    public bool Finished { get; private set; }
    public bool Won { get; private set; }
    public bool DepotSelected { get; private set; }
    public int Delivered { get; private set; }
    public int Score { get; private set; }
    public int Stars { get; private set; }
    public float TimeLeft { get; private set; }
    public string Message { get; private set; }
    public List<DeliveryStop> Orders { get; } = new List<DeliveryStop>();

    private Camera gameCamera;

    void Start()
    {
        Time.timeScale = 1f;
        gameCamera = Camera.main;
        TimeLeft = shiftDuration;
        Message = "Deine Lieferschicht wartet.";
        for (int i = 0; i < trucks.Length; i++)
            trucks[i].ResetTruck(parkingSpots[i].position);
        UpdateMarkers();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        Running = true;
        Paused = false;
        Finished = false;
        Won = false;
        DepotSelected = false;
        Delivered = 0;
        Score = 0;
        Stars = 0;
        TimeLeft = shiftDuration;
        Orders.Clear();
        for (int i = 0; i < trucks.Length; i++)
            trucks[i].ResetTruck(parkingSpots[i].position);
        FillOrders();
        Message = "1. Klicke das orange Paketlager an.";
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame && Running)
            TogglePause();
        if (!Running || Paused) return;

        TimeLeft = Mathf.Max(0f, TimeLeft - Time.deltaTime);
        if (TimeLeft <= 0f)
        {
            EndShift(false);
            return;
        }

        ReturnFailedOrders();
        ReadMouseClick();
    }

    // Ein abgebrochener Auftrag kommt zurück in die Liste.
    void ReturnFailedOrders()
    {
        foreach (DeliveryTruck truck in trucks)
        {
            if (truck.AssignedTarget != null && !truck.IsBusy)
            {
                Orders.Insert(0, truck.AssignedTarget);
                if (Orders.Count > 3) Orders.RemoveAt(3);
                truck.ClearAssignment();
                Message = truck.truckName + ": Bitte den Auftrag erneut vergeben.";
                FillOrders();
            }
        }
    }

    // Ein Klick trifft entweder die Oberfläche oder ein Gebäude in der Stadt.
    void ReadMouseClick()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;
        Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            DeliveryStop stop = hit.collider.GetComponentInParent<DeliveryStop>();
            if (stop != null) SelectStop(stop);
        }
    }

    // Ablauf einer Bestellung: erst Lager, dann ein Haus aus der Liste.
    public void SelectStop(DeliveryStop stop)
    {
        if (!Running || Paused || stop == null) return;
        if (stop.isDepot)
        {
            if (FreeTruck() == null)
            {
                Message = "Alle Wagen sind unterwegs. Warte auf ihre Rückkehr.";
                return;
            }
            DepotSelected = true;
            Message = "2. Klicke ein markiertes Haus aus der Auftragsliste an.";
            PlaySound(selectSound);
            return;
        }
        if (!Orders.Contains(stop))
        {
            Message = "Für dieses Gebäude liegt gerade kein Auftrag vor.";
            return;
        }
        if (!DepotSelected)
        {
            Message = "Zuerst das orange Paketlager anklicken, dann das Zielhaus.";
            return;
        }
        DeliveryTruck available = FreeTruck();
        if (available == null)
        {
            DepotSelected = false;
            Message = "Alle Wagen sind unterwegs. Gleich ist wieder einer frei.";
            return;
        }
        if (!available.TryDeliver(depot, stop, this))
        {
            Message = "Das Ziel ist gerade nicht erreichbar. Bitte erneut versuchen.";
            return;
        }
        Orders.Remove(stop);
        DepotSelected = false;
        Message = available.truckName + " übernimmt: " + stop.stopName + ".";
        PlaySound(selectSound);
        FillOrders();
    }

    DeliveryTruck FreeTruck()
    {
        foreach (DeliveryTruck truck in trucks)
            if (!truck.IsBusy && truck.AssignedTarget == null) return truck;
        return null;
    }

    // Jeder Wagen merkt sich sein Ziel selbst. Eine zweite Auftragsliste ist unnötig.
    bool IsAssigned(DeliveryStop stop)
    {
        foreach (DeliveryTruck truck in trucks)
            if (truck.AssignedTarget == stop) return true;
        return false;
    }

    void FillOrders()
    {
        List<DeliveryStop> choices = new List<DeliveryStop>();
        foreach (DeliveryStop stop in destinations)
            if (!Orders.Contains(stop) && !IsAssigned(stop)) choices.Add(stop);
        while (Orders.Count < 3 && choices.Count > 0)
        {
            int index = Random.Range(0, choices.Count);
            Orders.Add(choices[index]);
            choices.RemoveAt(index);
        }
        UpdateMarkers();
    }

    void UpdateMarkers()
    {
        depot.Highlight(true);
        foreach (DeliveryStop stop in destinations) stop.Highlight(Running && Orders.Contains(stop));
    }

    // Der Wagen ruft diese Methode nach dem Ausladen direkt auf.
    public void OnDelivered(DeliveryStop target)
    {
        if (!Running) return;
        Delivered++;
        Score += 100;
        Message = "Zugestellt! " + target.stopName + " hat das Paket erhalten. +100";
        PlaySound(deliverySound);
        if (Delivered >= deliveryGoal) EndShift(true);
        else FillOrders();
    }

    void EndShift(bool success)
    {
        Running = false;
        Finished = true;
        Won = success;
        Paused = false;
        DepotSelected = false;
        if (success)
        {
            Score += Mathf.CeilToInt(TimeLeft) * 5;
            Stars = 1;
            if (TimeLeft >= shiftDuration * .15f) Stars = 2;
            if (TimeLeft >= shiftDuration * .4f) Stars = 3;
            string key = "BestScore_City" + cityNumber;
            PlayerPrefs.SetInt(key, Mathf.Max(Score, PlayerPrefs.GetInt(key, 0)));
            PlayerPrefs.Save();
        }
        UpdateMarkers();
        Time.timeScale = 0f;
    }

    public void TogglePause()
    {
        if (!Running) return;
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
    }

    public void Restart() { StartGame(); }
    public void ReturnToMenu() { LoadCity(cityNumber); }
    public void LoadCity(int city)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(city == 2 ? "HarborSide" : "LittleOakGame");
    }

    void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null) audioSource.PlayOneShot(sound);
    }
}
