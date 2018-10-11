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
    [SerializeField] Transform[] patrolHolders;

    [SerializeField] float timeBetweenWaves;

    [HideInInspector][SerializeField] UnityEvent onWaveChange;
    [HideInInspector][SerializeField] UnityEvent onLevelComplete;
    [HideInInspector][SerializeField] UnityEvent onCountdown;

    bool waveCompleted = false;
    bool levelCompleted = false;
    float searchCountdown = 1f;
    SpawnStates state;

    void Awake()
    {
        state = SpawnStates.COUNTING;
        TimeLeft = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnStates.WAITING)
        {
            if(waveCompleted || levelCompleted)
            {
                StartCountdown();
            }
            else if (!waveCompleted)
            {
                if (!EnemyIsAlive())
                {
                    waveCompleted = true;
                    if (nextWave < waves.Length - 1)
                    {
                        
                    }
                    else
                        levelCompleted = true;
                }
            }
            return;
        }

        if(TimeLeft <= 0)
        {
            if (state != SpawnStates.SPAWNING)
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
        onCountdown.Invoke();

        nextWave++;
        if (nextWave > waves.Length - 1)
        {
            OnLevelComplete.Invoke();

            gameObject.SetActive(false);
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
        int spawnHolder = Random.Range(0, spawnPoints.Length);
        
        Transform pointTransform = spawnPoints[spawnHolder];
        
        Transform go = Instantiate(enemy, pointTransform.position, pointTransform.rotation);

        if (enemy.name == "EnemySuperior") {
            go.GetComponent<PatrolRandom>().SetPoints(patrolHolders[patrolHolders.Length -1].GetComponentsInChildren<Transform>());
            return;
        }
        
        go.GetComponent<Patrol>().SetPoints(patrolHolders[spawnHolder].GetComponentsInChildren<Transform>());
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
    public UnityEvent OnLevelComplete
    {
        get { return onLevelComplete; }
    }
    public UnityEvent OnCountdown
    {
        get { return onCountdown; }
    }
}
