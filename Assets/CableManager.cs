using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class CableManager : MonoBehaviour
{
    [SerializeField] GameObject OriginPoint;
    [SerializeField] GameObject EndPoint;

    [SerializeField] Color closeColor;
    [SerializeField] Color farColor;
    [SerializeField] float maxDistance = 20f;

    private int currentMovingIndex = 1;
    private Checkpoint currentConnected;
    private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, OriginPoint.transform.position);
        lineRenderer.SetPosition(1, EndPoint.transform.position);
    }

    public Checkpoint GetCurrentlyConnectedCheckpoint()
    {
        return currentConnected;
    }

    public void MarkCheckpoint(Checkpoint checkpoint)
    {
        currentConnected = checkpoint;
        Vector3 currentPos = transform.position;
        int tweenIndex = currentMovingIndex;
        DOTween.To(() => checkpoint.gameObject.transform.position,
                   x => currentPos = x,
                   checkpoint.gameObject.transform.position,
                   1f)
               .OnUpdate(() => lineRenderer.SetPosition(tweenIndex, currentPos));
        currentMovingIndex++;
        lineRenderer.positionCount++;
    }


    // Update is called once per frame
    void Update()
    {
        CableUpdate();
    }

    void CableUpdate()
    {
        lineRenderer.SetPosition(currentMovingIndex, EndPoint.transform.position);
        float dist = Vector2.Distance(EndPoint.transform.position, lineRenderer.GetPosition(currentMovingIndex - 1));

        Color currColor = Color.Lerp(closeColor, farColor, Mathf.Clamp01(dist / maxDistance));
        lineRenderer.startColor = currColor;
        lineRenderer.endColor = currColor;
    }
}
