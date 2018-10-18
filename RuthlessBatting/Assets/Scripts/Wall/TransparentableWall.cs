using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentableWall : MonoBehaviour
{
    [Range(0, 0.99f)]
    [SerializeField] float levelOfTransparency = 0.3f;
    [SerializeField] string overWallLayer = "Over Wall";
    [SerializeField] string behindWallLayer = "Behind Wall";

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

        col.transform.parent.GetComponentInChildren<SpriteRenderer>().sortingLayerName = behindWallLayer;
    }

    void OnTriggerExit(Collider col)
    {
        countOfObjects--;
        if (countOfObjects == 0 && isTransparent)
            TurnOffTransparent();

        col.transform.parent.GetComponentInChildren<SpriteRenderer>().sortingLayerName = overWallLayer;
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
