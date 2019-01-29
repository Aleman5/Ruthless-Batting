using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public enum SceneEnum
{
    Menu = 0,
    StoryboardN1,
    Game3D,
    Level2,
    Loading,
    TotalScenes
};

public class SceneLoaderManager : MonoBehaviour
{
    private static SceneLoaderManager instance = null;

    SceneEnum currentScene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            currentScene = SaveLoad.saveGame.data.actualScene;
        else
            currentScene = SceneEnum.Menu;
    }

    public void Initialize()
    {

    }

    public void StartNewGame()
    {
        currentScene = SceneEnum.Menu;
        SaveLoad.NewGame();
    }

    public void LoadSavedGame()
    {
        currentScene = SaveLoad.saveGame.data.actualScene;
        SaveLoad.Load();
    }

    public void ReloadScene(Scene actualScene)
    {
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            SceneManager.LoadScene(actualScene.name);
        else
        {
            currentScene = SceneEnum.StoryboardN1;
            SceneManager.LoadScene(currentScene.ToString());
        }
    }

    /// <summary>
    /// It loads the "Loading" scene but the param is saved for future uses.
    /// </summary>
    public void LoadSavedScene()
    {
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            currentScene = --SaveLoad.saveGame.data.actualScene;
            SceneManager.LoadScene(SceneEnum.Loading.ToString());
        }
        else
            StartNewGame();
    }

    /// <summary>
    /// It loads the "Loading" scene but the param is saved for future uses.
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadNextScene(SceneEnum sceneIndex)
    {
        Debug.Log(sceneIndex.ToString());

        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            currentScene = sceneIndex;
            SceneManager.LoadScene(SceneEnum.Loading.ToString());
        }
        else
        {
            currentScene = SceneEnum.StoryboardN1;
            SceneManager.LoadScene(currentScene.ToString());
        }
    }
    
    /// <summary>
    /// Returns you to the saved scene.
    /// </summary>
    public void ReturnToGameplay()
    {
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            LoadNextScene(SaveLoad.saveGame.data.actualScene);
        else
            LoadNextScene(SceneEnum.StoryboardN1);
    }

    /// <summary>
    /// Returns you to the Menu.
    /// </summary>
    public void ReturnMenu()
    {
        SceneManager.LoadScene(SceneEnum.Menu.ToString());
    }
    
    public SceneEnum GetCurrentScene()
    {
        return currentScene;
    }

    public SceneEnum GetNextScene()
    {
        currentScene++;
        
        return currentScene;
    }

    public static SceneLoaderManager Instance
    {
        get
        {
            instance = FindObjectOfType<SceneLoaderManager>();
            if (instance == null)
            {
                GameObject go = new GameObject("Scene Loader Manager");
                instance = go.AddComponent<SceneLoaderManager>();
            }
            return instance;
        }
    }
}