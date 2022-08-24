using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerManager playerManager;

    [SerializeField, Range(0f, 50f)]
    private float maxForceMagnitude;
    [SerializeField]
    private float forceOnCollision;

    Transform camHolder;

    private void Start()
    {
        camHolder = playerManager.camHolder.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cam rotate right")
        {
            Vector3 direction = new Vector3(camHolder.rotation.eulerAngles.x, camHolder.rotation.eulerAngles.y + 90, camHolder.rotation.eulerAngles.z);
            Quaternion targetRotation = Quaternion.Euler(direction);
            Quaternion.Lerp(playerManager.camHolder.transform.rotation, targetRotation, 0);
            Debug.Log(other.name);
        }
        else if (other.tag == "cam rotate left")
        {
            playerManager.camHolder.transform.Rotate(Vector3.up.normalized * -90);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestructibleObj dInfo = collision.collider.GetComponent<DestructibleObj>();
        if (dInfo != null)
        {
            dInfo.DamageObj(playerManager.damageCapability);
            //dInfo.health -= playerManager.damageCapability;
            //Debug.Log(dInfo.gameObject.name + ", " + dInfo.health);

            Debug.DrawLine(collision.transform.position, -collision.relativeVelocity, Color.red);
            //Debug.Break();
            Vector3 force = -collision.relativeVelocity * forceOnCollision;
            if (force.magnitude > maxForceMagnitude)
            {
                force = force.normalized * maxForceMagnitude;
            }
            //Debug.Log(force.magnitude);
            dInfo.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
