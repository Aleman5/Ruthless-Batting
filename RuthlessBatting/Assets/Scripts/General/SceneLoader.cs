using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    private bool loadScene = false;
    [SerializeField] private Text loadingText;

    void Update()
    {

        if (loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
        }

        if (loadScene == true)
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneLoaderManager.Instance.GoToNextScene().ToString());
        Debug.Log(SceneLoaderManager.Instance.GetCurrentScene().ToString());
        while (!async.isDone)
            yield return null;
    }
}
