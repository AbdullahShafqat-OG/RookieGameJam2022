using UnityEngine;
using UnityEngine.AI;

public class MotherController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private bool debugMode;

    private Camera cam;
    private Animator animator;

    private void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (debugMode)
            DebugMode();
        else
            agent.SetDestination(targetTransform.position);

        animator.SetFloat("Blend", agent.velocity.magnitude / agent.speed);
    }

    private void DebugMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //animator.SetBool("Hit", true);
            Messenger.Broadcast(GameEvent.AMMI_CAUGHT_UP);
        }
    }

}
