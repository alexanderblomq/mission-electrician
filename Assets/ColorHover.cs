using UnityEngine;
//using UnityEngine.InputSystem;
using System.Collections;

public class ColorHover : MonoBehaviour
{
    private Collider2D collider;
    private SpriteRenderer sr;
    private Color startCol;
    [SerializeField] Color highlightColor;

    private bool fadingIn = false;
    private bool fadingOut = false;
    Coroutine routine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        startCol = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 worldPos = Camera.main.ScreenToWorldPoint(InputSystem.actions.FindAction("MousePosition").ReadValue<Vector2>());

        //if (!fadingIn && collider.bounds.Contains(worldPos))
        //{
        //    routine = StartCoroutine(ColorChange(2, startCol, highlightColor));
        //    fadingIn = true;
        //}
        //else if (!fadingOut && sr.color != startCol)
        //{
        //    StopCoroutine(routine);
        //    routine = StartCoroutine(ColorChange(0.3f, highlightColor, startCol));
        //    fadingOut = true;
        //}
    }

    private IEnumerator ColorChange(float seconds, Color startColor, Color endColor)
    {
        float elapsedTime = 0f;

        while (elapsedTime < seconds)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / seconds);

            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        fadingIn = false;
        fadingOut = false;
    }
}
