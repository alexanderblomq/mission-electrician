using UnityEngine;
using DG.Tweening;

public class CircleController : MonoBehaviour
{
    public Material material;
    [Range(0f, 1f)]
    public float radius = 0f;
    [SerializeField] bool OpenAtStart = true;

    [SerializeField] public float openTime = 6f;
    [SerializeField] public float closeTime = 6f;

    private void Awake()
    {
        if(OpenAtStart)
        {
            Open();
        }
    }

    public void Open()
    {
        DOTween.To(() => 0f, x => radius = x, 1f, openTime);
    }

    public void Close()
    {
        DOTween.To(() => 1f, x => radius = x, 0f, closeTime);
    }

    void Update()
    {
        if (material != null)
        {
            material.SetFloat("_Radius", radius);
        }
    }
}
