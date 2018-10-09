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
    [SerializeField] PauseController pauseScript;

    [Header("Canvas")]
    [SerializeField] RectTransform winScreen;
    [SerializeField] GameObject pauseCanvas;

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
        pauseScript.OnPause.AddListener(PauseGame);
        pauseScript.OnResume.AddListener(ContinueGame);
        pauseScript.OnReturn.AddListener(ReturnMenu);
    }

    void IsWin()
    {
        winScreen.gameObject.SetActive(true);
        gameWon = true;

        Time.timeScale = 0;
        //PauseGame();
    }

    void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ContinueGame()
    {
        if(winScreen.gameObject.activeInHierarchy)
            winScreen.gameObject.SetActive(false);

        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ReturnMenu()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
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
