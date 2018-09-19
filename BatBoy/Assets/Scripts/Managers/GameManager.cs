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

    void Start()
    {
        waveSpawnerScript.OnLevelComplete.AddListener(IsWin);
        PlayerHealthScript.OnDeath.AddListener(Restart);
    }

    void IsWin()
    {
        winScreen.gameObject.SetActive(true);

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
        Debug.Log("Perdiste");
        //Animacion de muerte de jugador
        //Input manager
        //PauseGame();
            SceneManager.LoadScene(1);
    }
}
