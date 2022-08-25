using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public SceneReference[] scenes;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void Load(string sceneName)
    {
        SceneReference scene = System.Array.Find(scenes, scene => scene.SceneName == sceneName);
        if (scene == null)
        {
            Debug.LogWarning("Scene Name: " + sceneName + " not found!");
            return;
        }
        SceneManager.LoadScene(scene.ScenePath);
    }

    public void Load(SceneReference scene)
    {
        SceneManager.LoadScene(scene.ScenePath);
    }

    public void LoadNext()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene + 1 < scenes.Length)
            SceneManager.LoadScene(scenes[currentScene + 1].ScenePath);
        else
            Debug.LogWarning("Scene Name: " + scenes[currentScene].SceneName + " is last scene!");
    }
}
