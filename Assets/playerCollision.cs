using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
{
    public PlayerManager playerManager;

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
        DestructibleObjInfo dInfo = collision.collider.GetComponent<DestructibleObjInfo>();
        if (dInfo != null)
        {
            dInfo.health -= playerManager.damageCapability;
            Debug.Log(dInfo.gameObject.name + ", " + dInfo.health);

            //collision.contacts[0]
        }
    }
}
