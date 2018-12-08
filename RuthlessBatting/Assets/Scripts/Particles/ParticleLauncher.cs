using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    [SerializeField] ParticleSystem particleLauncher;
    [SerializeField] Gradient particleColorGradient;
    [SerializeField] ParticleDecalPool splatDecalPool;

    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void Killed()
    {
        ParticleSystem.MainModule psMain = particleLauncher.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0.0f, 1.0f));
        particleLauncher.Emit(1);
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            splatDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
        }
    }
}
