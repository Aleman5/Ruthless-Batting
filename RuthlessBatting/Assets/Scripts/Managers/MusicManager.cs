using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    Audios sceneAudio;

    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void Play()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                sceneAudio = Audios.menu;
                break;
            case "StoryboardN1":
                sceneAudio = Audios.menu;
                break;
            case "Game3D":
                sceneAudio = Audios.nivel1;
                break;

            case "Level2":
                sceneAudio = Audios.nivel2;
                break;
        }

        AudioManager.Instance.RunAudio(sceneAudio);
    }

    public void Stop()
    {

    }

    static public MusicManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MusicManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("MusicManager");
                    instance = go.AddComponent<MusicManager>();
                }
            }
            return instance;
        }
    }
}
