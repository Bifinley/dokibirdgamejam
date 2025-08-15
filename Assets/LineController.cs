using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] newPoints)
    {
        points = newPoints;
        lineRenderer.positionCount = points.Length;
        this.points = newPoints;
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
