using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuertaInteractuable : MonoBehaviour
{
    private Interactuable interactuable;
    public Interactuable requisito;
    public GameObject objetoBloqueado;

    public string escenaDestino;
    public CanvasGroup fadeNegro;
    public GameObject manager;

    void Awake()
    {
        interactuable = GetComponent<Interactuable>();

        if (objetoBloqueado != null)
            objetoBloqueado.SetActive(false);

        if (fadeNegro != null)
            fadeNegro.alpha = 0;
    }

    void Update()
    {
        if (interactuable == null)
            return;

        if (interactuable.EstaAbierto)
        {
            if (requisito != null && !requisito.FueUsado)
            {
                MostrarBloqueo();
            }
            else
            {
                interactuable.Cerrar();
                StartCoroutine(CambiarEscenaConFade());
            }
        }
        else
        {
            OcultarBloqueo();
        }

        manager.SetActive(true);
    }

    void MostrarBloqueo()
    {
        if (objetoBloqueado != null && !objetoBloqueado.activeSelf)
            objetoBloqueado.SetActive(true);
    }

    void OcultarBloqueo()
    {
        if (objetoBloqueado != null && objetoBloqueado.activeSelf)
            objetoBloqueado.SetActive(false);
    }

    IEnumerator CambiarEscenaConFade()
    {
        if (fadeNegro != null)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                fadeNegro.alpha = t;
                yield return null;
            }
        }

        SceneManager.LoadScene(escenaDestino);
    }
}
