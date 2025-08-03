using UnityEngine;
using DG.Tweening;

public class Light : MonoBehaviour
{
    [SerializeField] GameObject lightAura;
    [SerializeField] float turnOnTime = 1f;
    [SerializeField] float onAlpha = 0.44f;
    public void TurnOn()
    {
        SpriteRenderer rend = lightAura.GetComponent<SpriteRenderer>();
        Color changer = rend.color;
        DOTween.To(()=>changer, x => changer = x, new Color(changer.r, changer.g, changer.b, onAlpha), turnOnTime)
            .OnUpdate(() => rend.color = changer);
    }

    public void TurnOff()
    {
        SpriteRenderer rend = lightAura.GetComponent<SpriteRenderer>();
        Color changer = rend.color;
        DOTween.To(() => changer, x => changer = x, new Color(changer.r, changer.g, changer.b, 0), turnOnTime)
            .OnUpdate(() => rend.color = changer);
    }
}
