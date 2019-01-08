using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button btnContinue;

    void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
            if (SaveLoad.saveGame.data.saveCreated)
                return;

        btnContinue.enabled = false;
        btnContinue.GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f);
    }

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
