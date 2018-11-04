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
            if (index > 0)
            {
                panels[index].SetActive(false);
                index--;
                panels[index].SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            panels[index].SetActive(false);
            index++;
            if (index == panels.Count)
                SaveLoad.NewGame();
            else
                panels[index].SetActive(true);
        }
    }
}
