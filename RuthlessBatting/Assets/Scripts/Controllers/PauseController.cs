using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    public static bool GameIsPause = false;
    [HideInInspector][SerializeField] UnityEvent onPause;
    [HideInInspector][SerializeField] UnityEvent onResume;
    [HideInInspector][SerializeField] UnityEvent onReturn;
    
    /*void Update()
    {
        if (GameIsPause)
            Resume();
        else
            Pause();
    }*/

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
        //StartCoroutine(ChangeLevel());
    }

    void OnEnable()
    {
        Pause();
    }

    void OnDisable()
    {
        Resume();
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

    /*IEnumerator ChangeLevel()
    {
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(2);//fadeTime);

        SceneManager.LoadScene("Menu");
    }*/
}
