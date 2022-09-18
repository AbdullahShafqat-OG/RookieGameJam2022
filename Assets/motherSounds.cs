using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motherSounds : MonoBehaviour
{
    [SerializeField]
    AudioClip[] daant, catching, win, lose;
    [SerializeField]
    AudioClip slap;

    AudioSource audioSource;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    void OnObjDestroyed()
    {
        int choice = Random.Range(0, daant.Length);

        if(GameManager.instance.score < GameManager.instance.targetScore)
        {
            StartCoroutine(playSound(daant, choice));
        }
    }

    void OnAmmiCaughtUp()
    {
        int choice = Random.Range(0, catching.Length);


        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(catching[choice]);
        }
        audioSource.PlayOneShot(slap);
    }

    IEnumerator playSound(AudioClip[] arr, int index)
    {
        yield return new WaitForSeconds(0.3f);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(arr[index]);
        }
    }
}
