using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSortingLayer : MonoBehaviour
{
    public string LAYER_NAME = "TopLayer";
    public int sortingOrder = 0;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        LAYER_NAME = sprite.sortingLayerName;

        if (sprite)
        {
            sprite.sortingOrder = sortingOrder;
            sprite.sortingLayerName = LAYER_NAME;
        }
    }
}
