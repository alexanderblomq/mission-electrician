using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float duration = 1.0f;
    private Vector2 startPos;
    [SerializeField] Transform endPos;

    private Vector2 currentPos;
    private Vector3 prevPos;

    private Vector2 velocity;
    private AudioSource audio;

    bool stopMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        startPos = transform.position;
        currentPos = startPos;
    }

    public void MoveToEnd()
    {
        StartCoroutine(LookWithCamera());
        MovePlatform(currentPos, endPos.position);
    }

    public void MoveToStart()
    {
        MovePlatform(currentPos, startPos);
    }

    public void MovePlatform(Vector2 from, Vector2 to)
    {
        audio.Play();   
        DOTween.To(() => from, x => currentPos = x, to, duration)
            .OnUpdate(() => {
                transform.position = currentPos;
            })
            .OnComplete(() => {
                audio.Stop();
                if (stopMoving) return;
            });

    }

    IEnumerator LookWithCamera()
    {
        CinemachineCamera cam = Camera.main.GetComponent<CinemachineCamera>();
        Transform prevtarget = cam.Target.TrackingTarget;
        cam.Target.TrackingTarget = transform;
        yield return new WaitForSeconds(3);
        cam.Target.TrackingTarget = prevtarget;
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
