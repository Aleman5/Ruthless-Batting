using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

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
