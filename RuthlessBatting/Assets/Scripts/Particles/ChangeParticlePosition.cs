using UnityEngine;

public class ChangeParticlePosition : MonoBehaviour
{
    ParticleLauncher pl;

    private void Start()
    {
        GameObject go = GameObject.Find("ParticleLauncher");

        if (!go)
        {
            enabled = false;
            return;
        }
        
        pl = go.GetComponent<ParticleLauncher>();
        GetComponentInParent<Health>().OnDeath.AddListener(Splash);
    }

    void Splash()
    {
        Vector3 vec = new Vector3(0.0f, 0.0f, 0.3f);
        pl.transform.position = transform.position + vec;
        pl.GetComponent<ParticleLauncher>().Killed();
    }
}
