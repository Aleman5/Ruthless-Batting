using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour {

    public static bool GameIsPause = false;
    [HideInInspector][SerializeField] UnityEvent onPause;
    [HideInInspector][SerializeField] UnityEvent onResume;
    [HideInInspector][SerializeField] UnityEvent onReturn;

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
    }

    public void Save()
    {
        SaveLoad.Save();
    }

    void OnEnable()
    {
        AudioManager.Instance.RunAudio(Audios.pausa_on);

        Pause();
    }

    void OnDisable()
    {
        AudioManager.Instance.RunAudio(Audios.pausa_off);

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
}
