using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] Animator creditsAnimator;

    void Update()
    {
        if (InputManager.Instance.GetPauseButton())
        {
            SceneLoaderManager.Instance.ReturnMenu();
        }

        StartCoroutine(GoToMenu());
    }

    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(47.0f);
        SceneLoaderManager.Instance.ReturnMenu();
    }
}
