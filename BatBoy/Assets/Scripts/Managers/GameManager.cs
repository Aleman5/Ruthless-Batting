using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawnerScript;
    [SerializeField] RectTransform winScreen;

    void Start()
    {
        waveSpawnerScript.OnLevelComplete.AddListener(IsWin);
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
}
