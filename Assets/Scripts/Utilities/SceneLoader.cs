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

    private void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt("currLevel");
        if(currentLevelIndex == 0 || currentLevelIndex >= scenes.Length)
        {
            currentLevelIndex = 3;
        }
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
        Debug.Log("Loading" + currentLevelIndex);

        PlayerPrefs.SetInt("currLevel", currentLevelIndex + 1);

        //int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentLevelIndex + 1 < scenes.Length)
            SceneManager.LoadScene(scenes[currentLevelIndex].ScenePath);
        else
        {
            //Debug.LogWarning("Scene Name: " + scenes[currentLevelIndex].SceneName + " is last scene!");

            currentLevelIndex = 3;
            SceneManager.LoadScene(scenes[3].ScenePath);
        }
    }

    public void ReloadScene()
    {
        Debug.Log("reloading: " + currentLevelIndex);
        //int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scenes[currentLevelIndex].ScenePath);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void SetCurrentLevel()
    {
        Debug.Log("before set: " + currentLevelIndex);

        currentLevelIndex++;
        if (currentLevelIndex >= scenes.Length)
        {
            currentLevelIndex = 3;
        }

        Debug.Log("after set: " + currentLevelIndex);
    }
}
