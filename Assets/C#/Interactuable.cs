using UnityEngine;

// Tipos de interacción posibles
public enum TipoInteraccion
{
    SoloClick,
    ConAcercamiento
}

public class Interactuable : MonoBehaviour
{
    public TipoInteraccion tipoInteraccion = TipoInteraccion.ConAcercamiento;

    public GameObject objetoInteractivo;
    public Transform puntoInteraccion;


    public Vector3 tamañoInteraccion = new Vector3(2f, 1.5f, 2f);

    public bool EstaAbierto { get; private set; }

    public bool FueUsado { get; protected set; }

    void Awake()
    {
        if (puntoInteraccion == null)
            puntoInteraccion = transform;

        if (objetoInteractivo != null)
            objetoInteractivo.SetActive(false);
    }

    public void Abrir()
    {
        if (objetoInteractivo == null || EstaAbierto)
            return;

        objetoInteractivo.SetActive(true);
        EstaAbierto = true;
        FueUsado = true;

        CamaDormir cama = GetComponent<CamaDormir>();
        if (cama != null)
            cama.Dormir();
    }

    public void Cerrar()
    {
        if (objetoInteractivo == null || !EstaAbierto)
            return;

        objetoInteractivo.SetActive(false);
        EstaAbierto = false;
    }

    public bool PosicionDentro(Vector3 posicion)
    {
        if (tipoInteraccion != TipoInteraccion.ConAcercamiento)
            return false;

        Vector3 centro = puntoInteraccion.position;
        Vector3 mitad = tamañoInteraccion * 0.5f;

        return
            Mathf.Abs(posicion.x - centro.x) <= mitad.x &&
            Mathf.Abs(posicion.y - centro.y) <= mitad.y &&
            Mathf.Abs(posicion.z - centro.z) <= mitad.z;
    }

    void OnDrawGizmosSelected()
    {
        if (tipoInteraccion != TipoInteraccion.ConAcercamiento)
            return;

        if (puntoInteraccion == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(puntoInteraccion.position, tamañoInteraccion);
    }
}
