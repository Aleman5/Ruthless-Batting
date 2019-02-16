using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    static PlayerPrefsManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start () {
        if (PlayerPrefs.HasKey("volume"))
        {
            AkSoundEngine.SetRTPCValue("volumen_musica", PlayerPrefs.GetFloat("volume"));
            AkSoundEngine.SetRTPCValue("volumen_sfx", PlayerPrefs.GetFloat("volume"));
        }

        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 1)
                AudioManager.Instance.ChangeSoundState(true);
            else
                AudioManager.Instance.ChangeSoundState(false);
        }

        if (PlayerPrefs.HasKey("screen"))
        {
            if (PlayerPrefs.GetInt("screen") == 1)
                Screen.fullScreen = true;
            else
                Screen.fullScreen = false;
        }
    }

    public void Initialize()
    {

    }

    static public PlayerPrefsManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<PlayerPrefsManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Player Prefs");
                    instance = go.AddComponent<PlayerPrefsManager>();
                }
            }
            return instance;
        }
    }
}
