using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparencyFromObject : MonoBehaviour
{
    [Range(0, 0.99f)]
    [SerializeField] float levelOfTransparency = 0.3f;

    SpriteRenderer spriteRenderer;
    bool isTransparent;

    string layerName;
    int sortingOrder;

    [SerializeField] string transparentLayerName;
    [SerializeField] int transparentSortingOrder;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerName = spriteRenderer.sortingLayerName;
        sortingOrder = spriteRenderer.sortingOrder;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("LimitCollider"))
            MakeTransparent();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("LimitCollider"))
            TurnOffTransparent();
    }

    void MakeTransparent()
    {
        if (spriteRenderer.material.color.a != levelOfTransparency)
        {
            Color color = spriteRenderer.material.color;
            color.a = levelOfTransparency;
            spriteRenderer.material.color = color;
            spriteRenderer.sortingLayerName = transparentLayerName;
            spriteRenderer.sortingOrder = transparentSortingOrder;
        }
    }

    void TurnOffTransparent()
    {
        Color color = spriteRenderer.material.color;
        color.a = 1.0f;
        spriteRenderer.material.color = color;
        spriteRenderer.sortingLayerName = layerName;
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
