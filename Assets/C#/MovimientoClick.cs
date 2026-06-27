using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MovimientoClick : MonoBehaviour
{
    private NavMeshAgent agente;

    public Animator animatorRigidbody;

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
                agente.SetDestination(hit.point);
            }
        }

        if (agente.velocity.magnitude > 0.1f)
        {
            animatorRigidbody.SetBool("Caminando", true);
        }
        else
        {
            animatorRigidbody.SetBool("Caminando", false);
        }
    }
}
