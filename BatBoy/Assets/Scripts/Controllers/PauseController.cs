using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public static bool GameIsPause = false;
    [SerializeField] UnityEvent onPause;
    [SerializeField] UnityEvent onResume;
    [SerializeField] UnityEvent onReturn;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        GameIsPause = false;
        OnResume.Invoke();
    }

    public void Pause()
    {
        GameIsPause = true;
        OnPause.Invoke();
    }

    public void LoadMenu()
    {
        GameIsPause = false;
        OnReturn.Invoke();
        StartCoroutine(ChangeLevel());
    }

    public UnityEvent OnPause
    {
        get { return onPause; }
    }

    public UnityEvent OnResume
    {
        get { return onResume; }
    }

    public UnityEvent OnReturn
    {
        get { return onReturn; }
    }

    IEnumerator ChangeLevel()
    {
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(3);//fadeTime);
        SceneManager.LoadScene("Menu");
    }
}
