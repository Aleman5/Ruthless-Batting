using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StoryController : MonoBehaviour {

    [SerializeField] List<GameObject> panels;
    int index = 0;

	void Awake ()
    {
        panels[index].SetActive(true);
	}

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PrevPanel();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            NextPanel();
        }

        if (InputManager.Instance.GetPauseButton())
            LoadNextScene();
    }

    public void PrevPanel()
    {
        if (index > 0)
        {
            panels[index].SetActive(false);
            index--;
            panels[index].SetActive(true);
        }
    }

    public void NextPanel()
    {
        panels[index].SetActive(false);
        index++;
        if (index == panels.Count)
            LoadNextScene();
        else
            panels[index].SetActive(true);
    }

    public void LoadNextScene()
    {
        if (!File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            SceneLoaderManager.Instance.StartNewGame();
        else
            SceneLoaderManager.Instance.LoadNextScene(SceneLoaderManager.Instance.GetCurrentScene());
    }
}
