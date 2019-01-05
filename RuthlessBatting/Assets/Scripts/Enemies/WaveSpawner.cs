using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

enum SpawnStates
{
    COUNTING,
    SPAWNING,
    WAITING
}

[System.Serializable]
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

    [HideInInspector] UnityEvent onWaveChange = new UnityEvent();
    [HideInInspector] UnityEvent onLevelComplete = new UnityEvent();
    [HideInInspector] UnityEvent onCountdown = new UnityEvent();

    bool waveCompleted = false;
    float searchCountdown = 1f;
    SpawnStates state;

    void Start()
    {
        state = SpawnStates.COUNTING;
        AudioManager.Instance.RunAudio(Audios.respiracion_normal);
        Invoke("StartCountdown", timeToStartFirstWave);
        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            for (int i = 0; i < waves.Length; i++)
            {
                if (waves[i].name == SaveLoad.saveGame.data.waveName)
                    nextWave = i - 1;
            }
        }

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
        AudioManager.Instance.RunAudio(Audios.respiracion_normal);

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
            if(transform.childCount == 0)
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
        Transform go = Instantiate(enemy, pointTransform.position, pointTransform.rotation, transform);

        if (enemy.name == "EnemySuperiorFSM")
        {
            go.GetComponent<IPatrol>().SetPoints(patrolHolders[patrolHolders.Length -1].GetComponentsInChildren<Transform>());
            return;
        }

        go.GetComponent<IPatrol>().SetPoints(patrolHolders[spawnHolder].GetComponentsInChildren<Transform>());
    }

    public string GetActualWaveName()
    {
        if (nextWave == -1)
            return waves[nextWave + 1].name;

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
