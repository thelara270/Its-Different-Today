using UnityEngine;
using System.Collections;

public class FadeAlIniciarEscena : MonoBehaviour
{
    [Header("Fade")]
    public CanvasGroup fadeNegro;
    public float duracionFade = 1f;

    void Start()
    {
        if (fadeNegro == null)
            return;

        fadeNegro.alpha = 1f;
        fadeNegro.blocksRaycasts = true;

        StartCoroutine(FadeEntrada());
    }

    IEnumerator FadeEntrada()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / duracionFade;
            fadeNegro.alpha = t;
            yield return null;
        }

        fadeNegro.alpha = 0f;
        fadeNegro.blocksRaycasts = false;
    }
}
