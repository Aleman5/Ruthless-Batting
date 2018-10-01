using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawnerScript;
    [SerializeField] RectTransform winScreen;
    [SerializeField] Health PlayerHealthScript;

    bool gameWon = false;

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
}
