using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class StoryController : MonoBehaviour
{
    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject lastPanel;
    [SerializeField] List<GameObject> panels;
    [SerializeField] int importantPanel;
    [SerializeField] Button firstButton;
    [SerializeField] Button secondButton;
    int index = 0;
    bool impPanelState = false;

	void Awake ()
    {
        panels[index].SetActive(true);
        firstButton.gameObject.SetActive(false);
 
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A) | (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            PrevPanel();
        }

        if (Input.GetKeyDown(KeyCode.D) | (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            NextPanel();
        }

        if (InputManager.Instance.GetPauseButton())
            LoadNextScene();

        /**/

        /**/
    }

    public void PrevPanel()
    {
        if (index > 0)
        {
            panels[index].SetActive(false);
            index--;
            panels[index].SetActive(true);
            secondButton.GetComponentInChildren<Image>().color = Color.white;
        }
        if (panels[index] == firstPanel)
            firstButton.gameObject.SetActive(false);
    }

    public void NextPanel()
    {
        if (panels[index] == lastPanel)
            secondButton.GetComponentInChildren<Image>().color = Color.red;
        else
            secondButton.GetComponentInChildren<Image>().color = Color.white;

        firstButton.gameObject.SetActive(true);
        panels[index].SetActive(false);
        index++;
        if (index == panels.Count)
            LoadNextScene();
        else
        {
            panels[index].SetActive(true);

            if (index == importantPanel && !impPanelState)
                switch (SceneLoaderManager.Instance.GetNextScene())
                {
                    case SceneEnum.StoryboardN1:
                        AudioManager.Instance.RunAudio(Audios.storyboard1b);
                        impPanelState = true;
                        break;
                    case SceneEnum.StoryboardN2:
                        AudioManager.Instance.RunAudio(Audios.storyboard2b);
                        impPanelState = true;
                        break;
                    case SceneEnum.StoryboardN3:
                        AudioManager.Instance.RunAudio(Audios.storyboard2b);
                        impPanelState = true;
                        break;
                    case SceneEnum.StoryboardN4:
                        AudioManager.Instance.RunAudio(Audios.storyboard2b);
                        impPanelState = true;
                        break;
                }
        }
    }

    public void LoadNextScene()
    {
        if (!File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            SceneLoaderManager.Instance.StartNewGame();
        else
            SceneLoaderManager.Instance.LoadNextScene(SceneLoaderManager.Instance.GetCurrentScene());
    }
}
