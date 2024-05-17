using System.Collections;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    Vector3 defaultScale;

    void Awake()
    {
        defaultScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        StartCoroutine(ScaleTo(0.9f * defaultScale, 0.1f));
    }

    void OnMouseExit()
    {
        StartCoroutine(ScaleTo(defaultScale, 0.2f));
    }

    IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
