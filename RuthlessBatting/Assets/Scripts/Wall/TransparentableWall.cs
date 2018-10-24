﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentableWall : MonoBehaviour
{
    [Range(0, 0.99f)]
    [SerializeField] float levelOfTransparency = 0.3f;
    [SerializeField] const string overWallLayer = "OverWall";
    [SerializeField] int overWallSortingOrder = 0;
    [SerializeField] const string behindWallLayer = "BehindWall";
    [SerializeField] int behindWallSortingOrder = 0;

    SpriteRenderer spriteRenderer;
    bool isTransparent;
    int countOfObjects;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter(Collider col)
    {
        countOfObjects++;
        if (countOfObjects == 1 && !isTransparent)
            MakeTransparent();

        SpriteRenderer sprRenderer = col.transform.parent.GetComponentInChildren<SpriteRenderer>();
        sprRenderer.sortingLayerName = behindWallLayer;
        sprRenderer.sortingOrder = behindWallSortingOrder;
    }

    void OnTriggerExit(Collider col)
    {
        countOfObjects--;
        if (countOfObjects == 0 && isTransparent)
            TurnOffTransparent();

        SpriteRenderer sprRenderer = col.transform.parent.GetComponentInChildren<SpriteRenderer>();
        sprRenderer.sortingLayerName = overWallLayer;
        sprRenderer.sortingOrder = overWallSortingOrder;
    }

    void MakeTransparent()
    {
        isTransparent = true;

        Color color = spriteRenderer.material.color;
        color.a = levelOfTransparency;
        spriteRenderer.material.color = color;
    }

    void TurnOffTransparent()
    {
        isTransparent = false;

        Color color = spriteRenderer.material.color;
        color.a = 1.0f;
        spriteRenderer.material.color = color;
    }
}
