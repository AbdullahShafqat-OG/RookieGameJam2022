using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMotherSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] winSounds, loseSounds;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Win Screen")
        {
            int choice = Random.Range(0, winSounds.Length);
            audioSource.PlayOneShot(winSounds[choice]);

        }
        else if(SceneManager.GetActiveScene().name == "Lose Screen")
        {
            int choice = Random.Range(0, loseSounds.Length);
            audioSource.PlayOneShot(loseSounds[choice]);

        }
    }


}
