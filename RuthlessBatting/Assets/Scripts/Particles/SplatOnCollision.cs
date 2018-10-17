﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem particleLauncher;
    [SerializeField] Gradient particleColorGradient;
    [SerializeField] ParticleDecalPool dropletDecalPool;

    List<ParticleCollisionEvent> collisionEvents;


    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            dropletDecalPool.ParticleHit(collisionEvents[i], particleColorGradient);
            i++;
        }
    }
}