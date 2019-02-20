﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTheLastFight : MonoBehaviour
{
    [SerializeField] GameObject gate3;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject hud;
    [SerializeField] ZoomWhenKilling mainCamera;
    [SerializeField] float newMaxSizeForCamera;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LimitCollider"))
        {
            gate3.SetActive(true);
            enemySpawner.SetActive(true);
            hud.SetActive(true);
            ui.SetActive(true);
            mainCamera.SetNewMaxSize(newMaxSizeForCamera);
            Destroy(gameObject);
        }
    }
}
