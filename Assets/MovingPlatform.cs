using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float duration = 1.0f;
    private Vector2 startPos;
    [SerializeField] Transform endPos;

    private Vector2 currentPos;
    private Vector3 prevPos;

    private Vector2 velocity;


    bool stopMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        currentPos = startPos;
    }

    public void MoveToEnd()
    {
        MovePlatform(currentPos, endPos.position);
    }

    public void MoveToStart()
    {
        MovePlatform(currentPos, startPos);
    }

    public void MovePlatform(Vector2 from, Vector2 to)
    {
        DOTween.To(() => from, x => currentPos = x, to, duration)
            .OnUpdate(() => {
                transform.position = currentPos;
            })
            .OnComplete(() => {
                if (stopMoving) return;
            });

    }

    void LateUpdate()
    {
        velocity = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
    }

    public Vector2 GetVelModifier()
    {
        Debug.Log("Velocity: " + velocity);
        return velocity;
    }

    void StopMoving()
    {
        stopMoving = true;
    }
}
