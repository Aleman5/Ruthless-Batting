using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

enum SpawnStates
{
    COUNTING,
    SPAWNING,
    WAITING
}

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public Transform enemy;
        public int count;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public float rate;
        public Enemy[] enemies;
    }

    [SerializeField] Wave[] waves;
    int nextWave = 0;

    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float timeBetweenWaves;
    [SerializeField] float waveCountdown;

    [SerializeField] UnityEvent onWaveChange;
    [SerializeField] UnityEvent onNewWaveWaiting;
    [SerializeField] UnityEvent onLevelComplete;

    bool waveCompleted = false;
    bool levelCompleted = false;
    float searchCountdown = 1f;
    float timeToCompleteTheLevel = 65f;
    SpawnStates state;

    void Awake()
    {
        state = SpawnStates.COUNTING;
        waveCountdown = timeBetweenWaves;

        TimeLeft = timeToCompleteTheLevel;
    }

    void Update()
    {
        if(state != SpawnStates.COUNTING)
        {
            TimeLeft -= Time.deltaTime;
        }

        if(state == SpawnStates.WAITING)
        {
            if(TimeLeft <= 0 || (waveCompleted && InputManager.Instance.GetNextWaveButton()) || levelCompleted)
            {
                StartCountdown();
            }
            else if (!waveCompleted)
            {
                if (!EnemyIsAlive())
                {
                    waveCompleted = true;
                    if (nextWave < waves.Length - 1)
                        onNewWaveWaiting.Invoke();
                    else
                        levelCompleted = true;
                }
            }
            return;
        }

        if(waveCountdown <= 0)
        {
            if (state != SpawnStates.SPAWNING)
            {
                TimeLeft = timeToCompleteTheLevel;

                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void StartCountdown()
    {
        state = SpawnStates.COUNTING;
        waveCountdown = timeBetweenWaves;

        nextWave++;
        if (nextWave > waves.Length - 1)
        {
            OnLevelComplete.Invoke();

            gameObject.SetActive(false);
        }
        else
        {
            onNewWaveWaiting.Invoke();
        }
        waveCompleted = false;
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

        if (state == SpawnStates.SPAWNING)
        {
            onWaveChange.Invoke();
        }

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for (int j = 0; j < wave.enemies[i].count; j++)
            {
                SpawnEnemy(wave.enemies[i].enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        
        state = SpawnStates.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public string GetActualWaveName()
    {
        return waves[nextWave].name;
    }

    public float TimeLeft { get; set; }

    public UnityEvent OnWaveChange
    {
        get { return onWaveChange; }
    }
    public UnityEvent OnNewWaveWaiting
    {
        get { return onNewWaveWaiting; }
    }
    public UnityEvent OnLevelComplete
    {
        get { return onLevelComplete; }
    }
}
