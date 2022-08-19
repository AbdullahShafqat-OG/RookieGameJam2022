using DevLocker.Utils;
using UnityEngine;

public class UtilsShowcase : MonoBehaviour
{
    public KeyCode audioByNameKey;
    public string audioByName;

    [Space]
    public KeyCode sceneByReferenceKey;
    public SceneReference sceneByReference;
    
    [Space]
    public KeyCode sceneByNameKey;
    public string sceneByName;

    [Space]
    public KeyCode sceneNextKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(audioByNameKey))
            AudioManager.instance.PlaySound(audioByName);

        if (Input.GetKeyDown(sceneByReferenceKey))
            SceneLoader.instance.Load(sceneByReference);

        if (Input.GetKeyDown(sceneByNameKey))
            SceneLoader.instance.Load(sceneByName);

        if (Input.GetKeyDown(sceneNextKey))
            SceneLoader.instance.LoadNext();
    }
}
