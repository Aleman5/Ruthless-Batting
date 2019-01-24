using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneEnum
{
    Menu = 0,
    Loading,
    StoryboardN1,
    Game3D,
    Level2,
    TotalScenes
};

public class SceneLoaderManager : MonoBehaviour
{

    private static SceneLoaderManager instance = null;

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

    SceneEnum currentScene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentScene = 0;
    }

    public void Initialize()
    {

    }

    public string GetNextScene()
    {
        currentScene++;
        Debug.Log(currentScene);
        return currentScene.ToString();
    }
}