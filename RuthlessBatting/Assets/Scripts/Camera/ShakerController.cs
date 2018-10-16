using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ShakerController : MonoBehaviour {

    static ShakerController instance;
    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    public void AddToShake(Health health)
    {
        health.OnDeath.AddListener(Shake);
        health.OnDeath.AddListener(ZoomWhenKilling.Instance.ReduceSize);
    }

    void Shake()
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }

    static public ShakerController Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ShakerController>();
                if (!instance)
                {
                    GameObject go = new GameObject("ShakerController");
                    instance = go.AddComponent<ShakerController>();
                }
            }
            return instance;
        }
    }
}
