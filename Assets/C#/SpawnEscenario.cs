using UnityEngine;

public class SpawnEscenario : MonoBehaviour
{
    [Header("Escenarios")]
    public GameObject escenarioNormal;
    public GameObject[] escenariosConAnomalias;

    [Header("Spawn")]
    public Transform puntoSpawn; // opcional

    GameObject escenarioActual;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager no encontrado");
            return;
        }

        GameManager.Instance.PrepararNivel();
        CargarEscenario();
    }

    void CargarEscenario()
    {
        if (escenarioActual != null)
            Destroy(escenarioActual);

        Vector3 pos = puntoSpawn != null ? puntoSpawn.position : Vector3.zero;

        if (GameManager.Instance.estadoActual == EstadoAnomalia.Normal)
        {
            escenarioActual = Instantiate(
                escenarioNormal,
                pos,
                Quaternion.identity
            );
        }
        else
        {
            int index = Random.Range(0, escenariosConAnomalias.Length);
            escenarioActual = Instantiate(
                escenariosConAnomalias[index],
                pos,
                Quaternion.identity
            );
        }
    }
}
