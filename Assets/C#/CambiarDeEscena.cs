using UnityEngine.SceneManagement;
using UnityEngine;

public class CambiarDeEscena : MonoBehaviour
{
    public void PasarEscena(string escenaDestino)
    {
        SceneManager.LoadScene(escenaDestino);
    }
}
