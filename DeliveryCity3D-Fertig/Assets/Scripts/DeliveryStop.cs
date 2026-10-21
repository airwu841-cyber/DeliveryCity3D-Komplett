using UnityEngine;

public class DeliveryStop : MonoBehaviour
{
    public string stopName = "Haus";
    public Transform arrivalPoint;
    public bool isDepot;
    public Renderer marker;
    private Color normalColor;

    public Vector3 Position => arrivalPoint != null ? arrivalPoint.position : transform.position;

    void Awake()
    {
        if (marker != null)
            normalColor = marker.material.color;
    }

    public void Highlight(bool selected)
    {
        if (marker != null)
            marker.material.color = selected && !isDepot ? new Color(1f, .83f, .25f) : normalColor;
    }
}
