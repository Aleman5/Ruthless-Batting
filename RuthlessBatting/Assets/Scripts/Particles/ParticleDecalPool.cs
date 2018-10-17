using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    [SerializeField] int maxDecals = 100;
    [SerializeField] float decalSizeMin = 0.5f;
    [SerializeField] float decalSizeMax = 1.5f;

    ParticleSystem decalParticleSystem;
    int particleDecalDataIndex;
    ParticleDecalData[] particleData;
    ParticleSystem.Particle[] particles;

    void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f, 1f));

        particleDecalDataIndex++;
    }

    void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
