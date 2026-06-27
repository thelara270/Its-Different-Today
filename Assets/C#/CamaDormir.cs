using UnityEngine;
using System.Collections;

public class CamaDormir : MonoBehaviour
{
    public CanvasGroup fadeNegro;
    public float duracionFade = 1f;

    bool enProceso = false;

    public void Dormir()
    {
        if (enProceso) return;

        if (!GameManager.Instance.frutaElegidaEsteNivel)
            return;

        StartCoroutine(DormirRutina());
    }

    IEnumerator DormirRutina()
    {
        enProceso = true;
        fadeNegro.blocksRaycasts = true;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duracionFade;
            fadeNegro.alpha = t;
            yield return null;
        }

        GameManager.Instance.AvanzarDia();

        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / duracionFade;
            fadeNegro.alpha = t;
            yield return null;
        }

        fadeNegro.blocksRaycasts = false;
        enProceso = false;
    }
}
