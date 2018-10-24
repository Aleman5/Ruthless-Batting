using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ShakerController : MonoBehaviour {

    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    void Shake()
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
