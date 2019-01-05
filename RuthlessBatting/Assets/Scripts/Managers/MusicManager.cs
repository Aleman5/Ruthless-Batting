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
        // Acá preguntar por los nombres o Id de cada escena y mandar el audio correspondiente.

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                sceneAudio = Audios.nivel1;
                break;

            case "Level2":
                sceneAudio = Audios.nivel2;
                break;
        }

        AudioManager.Instance.RunAudio(sceneAudio);

        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void Stop()
    {
        this.gameObject.GetComponent<AudioSource>().Stop();
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
