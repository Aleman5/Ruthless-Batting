using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [Header("Scripts")]
    [SerializeField] Health PlayerHealthScript;
    [SerializeField] WaveSpawner waveSpawnerScript;

    [Header("Canvas")]
    [SerializeField] RectTransform winScreen;

    bool gameWon = false;

    void Awake()
    {
        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        waveSpawnerScript.OnLevelComplete.AddListener(IsWin);
        PlayerHealthScript.OnDeath.AddListener(Restart);
    }

    void IsWin()
    {
        winScreen.gameObject.SetActive(true);
        gameWon = true;

        PauseGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ContinueGame()
    {
        if(winScreen.gameObject.activeInHierarchy)
            winScreen.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    void Restart()
    {
        //Animacion de muerte de jugador
        //Input manager
        //PauseGame();
            SceneManager.LoadScene(1);
    }

    void Update()
    {
        if(gameWon && InputManager.Instance.GetActionButton())
            SceneManager.LoadScene(0);
    }

    static public GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
}
