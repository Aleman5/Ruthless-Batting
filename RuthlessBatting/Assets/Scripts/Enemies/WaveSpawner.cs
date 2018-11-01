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
    [SerializeField] float timeToStartFirstWave;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float rate;

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
        public Enemy[] enemies;
    }

    [SerializeField] Wave[] waves;
    int nextWave = -1;

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform[] patrolHolders;

    [HideInInspector][SerializeField] UnityEvent onWaveChange;
    [HideInInspector][SerializeField] UnityEvent onLevelComplete;
    [HideInInspector][SerializeField] UnityEvent onCountdown;

    bool waveCompleted = false;
    float searchCountdown = 1f;
    SpawnStates state;

    void Start()
    {
        state = SpawnStates.COUNTING;
        Invoke("StartCountdown", timeToStartFirstWave);
    }

    void Update()
    {
        if(state == SpawnStates.WAITING)
        {
            if(waveCompleted)
            {
                StartCountdown();
            }
            else if (!waveCompleted)
            {
                if (!EnemyIsAlive())
                {
                    waveCompleted = true;
                }
            }
            return;
        }

        if(TimeLeft <= -2)
        {
            if (state != SpawnStates.SPAWNING && nextWave != -1)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            TimeLeft -= Time.deltaTime;
        }
    }

    void StartCountdown()
    {
        state = SpawnStates.COUNTING;
        TimeLeft = timeBetweenWaves;

        nextWave++;

        if (nextWave > waves.Length - 1)
        {
            OnLevelComplete.Invoke();

            gameObject.SetActive(false);
        }
        else
        {
            onCountdown.Invoke();
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
                yield return new WaitForSeconds(1f / rate);
            }
        }
        
        state = SpawnStates.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        int spawnHolder = Random.Range(0, spawnPoints.Length);
        
        Transform pointTransform = spawnPoints[spawnHolder];
        Transform go = Instantiate(enemy, pointTransform.position, pointTransform.rotation);

        if (enemy.name == "EnemySuperiorFSM")
        {
            go.GetComponent<IPatrol>().SetPoints(patrolHolders[patrolHolders.Length -1].GetComponentsInChildren<Transform>());
            return;
        }

        go.GetComponent<IPatrol>().SetPoints(patrolHolders[spawnHolder].GetComponentsInChildren<Transform>());
    }

    public string GetActualWaveName()
    {
        return waves[nextWave].name;
    }

    public int GetWavesCount()
    {
        return waves.Length;
    }

    public float TimeLeft { get; set; }

    public UnityEvent OnWaveChange
    {
        get { return onWaveChange; }
    }
    public UnityEvent OnLevelComplete
    {
        get { return onLevelComplete; }
    }
    public UnityEvent OnCountdown
    {
        get { return onCountdown; }
    }
}
