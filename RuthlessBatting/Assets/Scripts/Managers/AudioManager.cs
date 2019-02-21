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
                RunAudio(Audios.menu);
                break;
            case SceneEnum.StoryboardN1:
                RunAudio(Audios.storyboard1a);
                break;
            case SceneEnum.Level1:
                RunAudio(Audios.nivel1);
                break;
            case SceneEnum.StoryboardN2:
                RunAudio(Audios.storyboard2a);
                break;
            case SceneEnum.Level2:
                RunAudio(Audios.nivel2);
                break;
            case SceneEnum.StoryboardN3:
                RunAudio(Audios.storyboard2b);
                break;
            case SceneEnum.Level3:
                RunAudio(Audios.nivel3);
                break;
            case SceneEnum.StoryboardN4:
                RunAudio(Audios.storyboard2b);
                break;
            case SceneEnum.Credits:
                RunAudio(Audios.menu);
                break;

        }
    }

    public void RunAudio(Audios audio)
    {
        if (audio == Audios.menu   || 
            audio == Audios.nivel1 || 
            audio == Audios.nivel2 || 
            audio == Audios.nivel3 ||
            audio == Audios.storyboard1a ||
            audio == Audios.storyboard2a ||
            audio == Audios.storyboard1b ||
            audio == Audios.storyboard2b
            )
            AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent(audio.ToString(), gameObject);
    }

    /// <summary>
    /// True = NextCanvas() else False = PrevCanvas()
    /// </summary>
    /// <param name="isClickOrHighlight"></param>
    public void RunButtonClick(bool isClickOrHighlight)
    {
        if (isClickOrHighlight)
            AkSoundEngine.PostEvent(Audios.menu_clic.ToString(), gameObject);
        else
            AkSoundEngine.PostEvent(Audios.menu_atras.ToString(), gameObject);
    }

    public void GamePaused()
    {
        paused = !paused;
        if (paused)
            AkSoundEngine.PostEvent(Audios.pausa_on.ToString(), gameObject);
        else
            AkSoundEngine.PostEvent(Audios.pausa_off.ToString(), gameObject);
    }

    public void ChangeSoundState(bool isActive)
    {
        if (isActive)
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
