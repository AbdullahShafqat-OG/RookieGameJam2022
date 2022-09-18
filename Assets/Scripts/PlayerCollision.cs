using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerManager playerManager;

    [SerializeField]
    private GameObject[] particleEffects;

    [SerializeField, Range(0f, 50f)]
    private float maxForceMagnitude;
    [SerializeField]
    private float forceOnCollision;
    [SerializeField]
    private GameObject animatedBoi, ragdollBoi;

    [SerializeField]
    private float scoreMultiplierStep;
    [SerializeField]
    private GameObject scorePopup;

    Transform camHolder;

    Vector3 hitPoint;


    bool playingBonk;

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
    private void Start()
    {
        camHolder = playerManager.camHolder.transform;
        playingBonk = false;
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

        if(collision.transform.tag == "Destructible" || dInfo != null)
        {
            this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<PlayerSoundManager>().bamboo, 1f);
            if (dInfo.health <= 1)
            {
                if (dInfo.objectType == "wood")
                {
                    this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<PlayerSoundManager>().woodBreak, 0.8f);
                }
                else if(dInfo.objectType == "glass")
                {
                    this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<PlayerSoundManager>().glassBreak);
                }
            }

            Messenger.Broadcast(GameEvent.HitDestructibleObject);

            GameManager.instance.scoreMultiplier += scoreMultiplierStep;

            int newHitScore = (int)((GameManager.instance.hitScore * (int)GameManager.instance.scoreMultiplier) * ((this.GetComponent<Rigidbody>().velocity.magnitude + 1) / playerManager.playerMovement.forwardSpeed));
            GameManager.instance.score += newHitScore;
            //GameManager.instance.score += (GameManager.instance.hitScore * (int)GameManager.instance.scoreMultiplier);


            var tempPopup = Instantiate(scorePopup, collision.contacts[0].point, scorePopup.transform.rotation);
            tempPopup.GetComponent<pointsPopup>().score = newHitScore;
        }
        else if(collision.transform.tag == "Indestructible")
        {
            if (!playingBonk)
            {
                playingBonk = true;
                Invoke("allowBonk", 0.1f);
                this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<PlayerSoundManager>().bonk, 0.1f);
            }
            //int newHitScore = (int)((GameManager.instance.hitScore * 2 * ((this.GetComponent<Rigidbody>().velocity.magnitude + 1) / playerManager.playerMovement.forwardSpeed)));
            //newHitScore *= -1;
            //GameManager.instance.score += newHitScore;
            ////GameManager.instance.score += (GameManager.instance.hitScore * (int)GameManager.instance.scoreMultiplier);


            //var tempPopup = Instantiate(scorePopup, collision.contacts[0].point, scorePopup.transform.rotation);
            //tempPopup.GetComponent<pointsPopup>().score = newHitScore;
        }
        if (dInfo != null)
        {
            hitPoint = collision.GetContact(0).point;
            Instantiate(particleEffects[Random.Range(0, particleEffects.Length)], hitPoint, Quaternion.identity);

            //Debug.Break();

            dInfo.DamageObj(playerManager.damageCapability);

            Vector3 force = -collision.relativeVelocity * forceOnCollision;

            if (force.magnitude > maxForceMagnitude)
            {
                force = force.normalized * maxForceMagnitude;
            }

            dInfo.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    void allowBonk()
    {
        playingBonk = false;
    }

    void OnObjDestroyed()
    {

    }
    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in Player Collision");
        animatedBoi.SetActive(false);
        ragdollBoi.SetActive(true);

        this.GetComponent<AudioSource>().Stop();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitPoint, 0.2f);
    }
}
