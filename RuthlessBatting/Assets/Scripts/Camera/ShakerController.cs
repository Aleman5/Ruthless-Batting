using UnityEngine;
using EZCameraShake;

public class ShakerController : MonoBehaviour {

    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;

    public void Shake()
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
