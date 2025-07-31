using UnityEngine;

public class CableManager : MonoBehaviour
{
    [SerializeField] GameObject OriginPoint;
    [SerializeField] GameObject EndPoint;

    private int currentMovingIndex = 1;

    private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, OriginPoint.transform.position);
        lineRenderer.SetPosition(1, EndPoint.transform.position);
    }

    void MarkPosition()
    {
        currentMovingIndex++;
        lineRenderer.positionCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            MarkPosition();
        }
        lineRenderer.SetPosition(currentMovingIndex, EndPoint.transform.position);
    }
}
