using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play()
    {
        StartCoroutine(ChangeScene());
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void Load()
    {
        SaveLoad.Load();
    }
    public void NewGame()
    {
        SaveLoad.NewGame();
    }
    IEnumerator ChangeScene()
    {
        //float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
