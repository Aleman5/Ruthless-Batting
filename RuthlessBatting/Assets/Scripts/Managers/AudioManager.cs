using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    public void RunAudio(Audios audio)
    {
        AkSoundEngine.PostEvent(audio.ToString(), gameObject);
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
