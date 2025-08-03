using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [Tooltip("The main camera controlled by Cinemachine.")]
    public Camera targetCamera;

    private Camera myCamera;

    void OnEnable()
    {
        myCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targetCamera == null || myCamera == null)
            return;

        // Match position
        transform.position = targetCamera.transform.position;

        // Match rotation
        transform.rotation = targetCamera.transform.rotation;

        // Match orthographic size if 2D
        if (targetCamera.orthographic)
        {
            myCamera.orthographic = true;
            myCamera.orthographicSize = targetCamera.orthographicSize;
        }
    }
}
