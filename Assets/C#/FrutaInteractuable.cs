using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FrutaInteractuable : MonoBehaviour
{
    [Header("Fruta")]
    public TipoFruta tipoFruta;

    [Header("Fade")]
    public CanvasGroup fadeNegro;
    public float duracionFade = 1f;

    [Header("Escena")]
    public string escenaInicial = "Cuarto";

    Interactuable interactuable;

    void Awake()
    {
        interactuable = GetComponent<Interactuable>();

        if (fadeNegro != null)
        {
            fadeNegro.alpha = 0;
            fadeNegro.blocksRaycasts = false;
        }
    }
 
    public void ConfirmarFruta()
    {
        if (GameManager.Instance.frutaElegidaEsteNivel)
            return;

        GameManager.Instance.RegistrarFruta(tipoFruta);

        if (!GameManager.Instance.FrutaCorrecta())
        {
            GameManager.Instance.ReiniciarJuego();
        }

        // Cerramos el panel
        if (interactuable != null)
            interactuable.Cerrar();

        StartCoroutine(FadeYVolver());
    }

    public void CancelarFruta()
    {
        if (interactuable != null)
            interactuable.Cerrar();
    }

    IEnumerator FadeYVolver()
    {
        if (fadeNegro != null)
        {
            fadeNegro.blocksRaycasts = true;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duracionFade;
                fadeNegro.alpha = t;
                yield return null;
            }
        }

        SceneManager.LoadScene(escenaInicial);
    }
}
