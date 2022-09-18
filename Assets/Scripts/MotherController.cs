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
    private void Awake()
    {
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in Mother Controller");
        animator.SetBool("Hit", true);
    }
    private void OnObjDestroyed()
    {
        if(GameManager.instance.score > GameManager.instance.targetScore)
        {
            agent.speed = 0;
        }
        //this.GetComponent<NavMeshAgent>().stoppingDistance = 10;
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
