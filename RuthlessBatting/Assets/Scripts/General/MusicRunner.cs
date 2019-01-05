using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRunner : MonoBehaviour
{
    [SerializeField] Audios[] audiosToRun;

    void Start()
    {
        foreach (Audios audio in audiosToRun)
            AudioManager.Instance.RunAudio(audio);
    }
}
