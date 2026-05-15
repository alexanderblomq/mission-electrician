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
    [SerializeField] Vector3 cableStartOffset;

    [SerializeField] Vector3 cableStartOffsetFlipped;

    [SerializeField] GameObject linePrefab;

    private Vector3 offset;

    private Checkpoint currentConnected;

    private LineRenderer currentMoving;

    [SerializeField] List<LineRenderer> lines = new List<LineRenderer>();

    private bool crossed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateLine(OriginPoint.transform.position, EndPoint.transform.position);
    }

    public void flipOffset(bool flipped)
    {
        if(flipped)
        {
            offset = cableStartOffsetFlipped;
        }
        else
        {
            offset = cableStartOffset;
        }
    }

    public LineRenderer CreateLine(Vector2 start, Vector2 end)
    {
        LineRenderer line = Instantiate(linePrefab, transform).GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        lines.Add(line);
        currentMoving = line;
        return line;
    }

    public Checkpoint GetCurrentlyConnectedCheckpoint()
    {
        return currentConnected;
    }

    public void MarkCheckpoint(Checkpoint checkpoint)
    {
        currentConnected = checkpoint;
        Vector3 currentPos = transform.position;

        LineRenderer tweening = currentMoving;
        DOTween.To(() => currentPos,
                   x => currentPos = x,
                   checkpoint.gameObject.transform.position,
                   1f)
               .OnUpdate(() => tweening.SetPosition(1, currentPos))
               .SetEase(Ease.OutCirc);

        CreateLine(currentConnected.transform.position, currentPos);
    }


    // Update is called once per frame
    void Update()
    {
        CableUpdate();
    }

    void CableUpdate()
    {
        currentMoving.SetPosition(1, EndPoint.transform.position + offset);
        float dist = Vector2.Distance(EndPoint.transform.position, currentMoving.GetPosition(0));

        Color currColor = Color.Lerp(closeColor, farColor, Mathf.Clamp01(dist / maxDistance));

        if (!crossed && CheckCableCrossing())
        {
            crossed = true;
            currColor = Color.red;
            GetComponentInParent<PlayerController>().Electrocute();
        }
        else if(crossed && !CheckCableCrossing()) {
            crossed = false;
        }

            currentMoving.startColor = currColor;
        currentMoving.endColor = currColor;
    }

    Vector2 AdjustedLineStart(Vector2 a, Vector2 b, float margin)
    {
        return Vector2.Lerp(a, b, margin);
    }

    Vector2 AdjustedLineEnd(Vector2 a, Vector2 b, float margin)
    {
        return Vector2.Lerp(b, a, margin);
    }


    bool DoLinesIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
    {
        float p0_x, p0_y, p1_x, p1_y, p2_x, p2_y, p3_x, p3_y;
        p0_x = a1.x; p0_y = a1.y;
        p1_x = a2.x; p1_y = a2.y;
        p2_x = b1.x; p2_y = b1.y;
        p3_x = b2.x; p3_y = b2.y;


        float s1_x, s1_y, s2_x, s2_y;
        s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
        s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

        float s, t;
        s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
        t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            return true;
        }
        return false;
    }

    bool CheckCableCrossing()
    {
        if (currentMoving == null) return false;

        Vector2 a1 = AdjustedLineStart(currentMoving.GetPosition(0), currentMoving.GetPosition(1), 0.1f);
        Vector2 a2 = AdjustedLineEnd(currentMoving.GetPosition(0), currentMoving.GetPosition(1), 0.1f);

        if (Vector2.Distance(a1, a2) < 3) return false;

        foreach (var line in lines)
        {
            if (line == currentMoving) continue; // skip current line

            Vector2 b1, b2;

            if(line == lines[lines.Count - 2])
            {
                b1 = AdjustedLineStart(line.GetPosition(0), line.GetPosition(1), 0.1f);
                b2 = AdjustedLineEnd(line.GetPosition(0), line.GetPosition(1), 0.1f);
            }

            b1 = line.GetPosition(0);
            b2 = line.GetPosition(1);

            if (DoLinesIntersect(a1, a2, b1, b2))
            {
                return true;
                // Handle logic here: destroy, reject, warn, etc.
            }
        }
        return false;
    }

}
