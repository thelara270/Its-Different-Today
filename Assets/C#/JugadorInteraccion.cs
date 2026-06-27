using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class JugadorInteraccion : MonoBehaviour
{
    private NavMeshAgent agente;

    private Interactuable interactuableActual;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit))
            {
                Interactuable nuevo = hit.collider.GetComponentInParent<Interactuable>();

                if (nuevo != null)
                {
                    CambiarInteractuable(nuevo);

                    if (nuevo.tipoInteraccion == TipoInteraccion.SoloClick)
                    {
                        agente.isStopped = true;
                        nuevo.Abrir();
                    }
                    else
                    {
                        agente.isStopped = false;
                        agente.SetDestination(nuevo.puntoInteraccion.position);
                    }
                }
                else
                {
                    CancelarInteraccion();
                    agente.isStopped = false;
                    agente.SetDestination(hit.point);
                }
            }
        }

        if (interactuableActual != null &&
            interactuableActual.tipoInteraccion == TipoInteraccion.ConAcercamiento)
        {
            if (interactuableActual.PosicionDentro(transform.position))
            {
                if (!interactuableActual.EstaAbierto)
                {
                    agente.isStopped = true;
                    interactuableActual.Abrir();
                }
            }
            else
            {
                if (interactuableActual.EstaAbierto)
                    CancelarInteraccion();
            }
        }
    }

    void CambiarInteractuable(Interactuable nuevo)
    {
        if (interactuableActual == nuevo)
            return;

        if (interactuableActual != null)
            interactuableActual.Cerrar();

        interactuableActual = nuevo;
    }

    void CancelarInteraccion()
    {
        if (interactuableActual != null)
            interactuableActual.Cerrar();

        interactuableActual = null;
    }

    public void CerrarDesdeUI()
    {
        if (interactuableActual == null)
            return;

        interactuableActual.Cerrar();

        agente.isStopped = false;

        interactuableActual = null;
    }
}
