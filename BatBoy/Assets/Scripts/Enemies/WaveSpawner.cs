using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnStates
{
    COUNTING,
    SPAWNING,
    WAITING
}

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    [SerializeField] Wave[] waves;
    private int nextWave = 0;

    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float timeBetweenWaves;
    [SerializeField] float waveCountdown;

    float searchCountdown = 1f;

    SpawnStates state;

    void Awake()
    {
        state = SpawnStates.COUNTING;
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnStates.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnStates.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnStates.COUNTING;
        waveCountdown = timeBetweenWaves;

        nextWave++;
        if(nextWave > waves.Length - 1)
        {
            nextWave = 0;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnStates.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnStates.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
