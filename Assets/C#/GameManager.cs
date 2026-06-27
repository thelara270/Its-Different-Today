using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum EstadoAnomalia
{
    Normal,
    ConAnomalia
}

public enum TipoFruta
{
    FrutaA,
    FrutaB
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action<int> OnNivelCambiado;

    [Header("Progreso")]
    public int nivelActual = 1;
    public int nivelesParaGanar = 5;

    [Header("Frutas")]
    public TipoFruta frutaInicial;
    public bool frutaInicialElegida;

    public TipoFruta frutaElegidaEnNivel;
    public bool frutaElegidaEsteNivel;

    [Header("Estado")]
    public EstadoAnomalia estadoActual;

    [Header("Escenas")]
    public string escenaVictoria;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PrepararNivel(); // primer día
    }

    public void PrepararNivel()
    {
        frutaElegidaEsteNivel = false;
        frutaElegidaEnNivel = default;

        if (nivelActual == 1)
            estadoActual = EstadoAnomalia.Normal;
        else if (nivelActual == 2)
            estadoActual = EstadoAnomalia.ConAnomalia;
        else
            estadoActual = UnityEngine.Random.value > 0.5f
                ? EstadoAnomalia.ConAnomalia
                : EstadoAnomalia.Normal;

        Debug.Log($"Dia {nivelActual} | Estado: {estadoActual}");

        OnNivelCambiado?.Invoke(nivelActual);
    }

    public void RegistrarFruta(TipoFruta fruta)
    {
        frutaElegidaEnNivel = fruta;
        frutaElegidaEsteNivel = true;

        if (!frutaInicialElegida)
        {
            frutaInicial = fruta;
            frutaInicialElegida = true;
            Debug.Log($"Fruta inicial elegida: {frutaInicial}");
        }
    }

    public bool FrutaCorrecta()
    {
        bool hayAnomalia = estadoActual == EstadoAnomalia.ConAnomalia;

        return
            (!hayAnomalia && frutaElegidaEnNivel == frutaInicial) ||
            (hayAnomalia && frutaElegidaEnNivel != frutaInicial);
    }

    public void AvanzarDia()
    {
        if (nivelActual > nivelesParaGanar)
        {
            Debug.Log("Ultimo dia completado");
            nivelActual = 1;
            SceneManager.LoadScene(escenaVictoria);
            return;
        }

        nivelActual++;
        PrepararNivel();
    }

    public void ReiniciarJuego()
    {
        Debug.Log("Fruta incorrecta  reinicio al dia 1");

        nivelActual = 1;
        estadoActual = EstadoAnomalia.Normal;
        frutaElegidaEsteNivel = false;
        frutaElegidaEnNivel = default;

        PrepararNivel();
    }
}
