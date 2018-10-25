using System.Collections;
using System.Collections.Generic;
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
        pl.transform.position = transform.position;
        pl.GetComponent<ParticleLauncher>().Killed();
    }
}
