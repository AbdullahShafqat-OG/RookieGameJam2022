using DevLocker.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public SceneReference menuScene;

    public SceneReference[] scenes;

    private int currentLevelIndex;

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

    public void LoadNextLevel()
    {
        //int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 < scenes.Length)
            SceneManager.LoadScene(scenes[currentLevelIndex].ScenePath);
        else
        {
            //Debug.LogWarning("Scene Name: " + scenes[currentLevelIndex].SceneName + " is last scene!");
            SceneManager.LoadScene(menuScene.ScenePath);
        }
    }

    public void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scenes[currentScene].ScenePath);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void SetCurrentLevel()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
