using System.Collections;
using UnityEngine;

public class CanvasFadeInOut : MonoBehaviour
{
    public Canvas canvas; // Referensi ke Canvas yang akan di-fade
    public float fadeDuration = 1f; // Durasi transisi fade in/out
    public float delayBeforeFadeOut = 5f; // Delay sebelum fade out
    public bool enableFadeIn = true; // Apakah fade in diaktifkan
    public bool enableFadeOut = true; // Apakah fade out diaktifkan

    private CanvasRenderer[] canvasRenderers;

    private void Start()
    {
        // Ambil semua CanvasRenderer dari anak-anak Canvas
        canvasRenderers = canvas.GetComponentsInChildren<CanvasRenderer>();

        if (enableFadeIn)
        {
            StartCoroutine(FadeInCanvas());
        }
        else if (enableFadeOut)
        {
            StartCoroutine(FadeOutCanvas());
        }
    }

    private IEnumerator FadeInCanvas()
    {
        // Pastikan canvas aktif
        canvas.gameObject.SetActive(true);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            foreach (var renderer in canvasRenderers)
            {
                renderer.SetAlpha(alpha);
            }

            yield return null;
        }

        foreach (var renderer in canvasRenderers)
        {
            renderer.SetAlpha(1f);
        }

        if (enableFadeOut)
        {
            yield return new WaitForSeconds(delayBeforeFadeOut);
            StartCoroutine(FadeOutCanvas());
        }
    }

    private IEnumerator FadeOutCanvas()
    {
        yield return new WaitForSeconds(delayBeforeFadeOut);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            foreach (var renderer in canvasRenderers)
            {
                renderer.SetAlpha(alpha);
            }

            yield return null;
        }

        foreach (var renderer in canvasRenderers)
        {
            renderer.SetAlpha(0f);
        }

        canvas.gameObject.SetActive(false);
    }
}
