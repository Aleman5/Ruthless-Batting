using System.Collections;
using System.Collections.Generic;
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
            StartGame();
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
            StartGame();
        else
            panels[index].SetActive(true);
    }

    public void StartGame()
    {
        SaveLoad.NewGame();
    }
}
