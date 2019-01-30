using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    bool soundOn;
    bool paused;

    void Start()
    {
        paused = false;
        soundOn = true;
        if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetInt("Sound") == 0)
            soundOn = false;

        SceneEnum sE = (SceneEnum)System.Enum.Parse(typeof(SceneEnum), SceneManager.GetActiveScene().name);

        switch (sE)
        {
            case SceneEnum.Menu:
                AudioManager.Instance.RunAudio(Audios.menu);
                break;
            case SceneEnum.StoryboardN1:
                AudioManager.Instance.RunAudio(Audios.menu);
                break;
            case SceneEnum.Game3D:
                AudioManager.Instance.RunAudio(Audios.nivel1);
                break;
            case SceneEnum.Level2:
                AudioManager.Instance.RunAudio(Audios.nivel2);
                break;
        }
    }

    public void RunAudio(Audios audio)
    {
        if (audio == Audios.menu || audio == Audios.nivel1 || audio == Audios.nivel2)
            AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent(audio.ToString(), gameObject);
    }

    public void GamePaused()
    {
        paused = !paused;
        if (paused)
            AkSoundEngine.PostEvent(Audios.pausa_on.ToString(), gameObject);
        else
            AkSoundEngine.PostEvent(Audios.pausa_off.ToString(), gameObject);
    }

    public void ChangeSoundState()
    {
        soundOn = !soundOn;
        if (soundOn)
            AkSoundEngine.PostEvent(Audios.musica_on.ToString(), gameObject);
        else
            AkSoundEngine.PostEvent(Audios.musica_off.ToString(), gameObject);
    }

    static public AudioManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<AudioManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Main Camera");
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }
}
