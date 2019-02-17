using UnityEngine;

public class TransparentableWall : MonoBehaviour
{
    [Range(0, 0.99f)]
    [SerializeField] float levelOfTransparency = 0.55f;
    [SerializeField] const string overWallLayer = "OverWall";
    [SerializeField] const string overWallUpLayer = "OverWallUp";
    [SerializeField] int overWallSortingOrder = 0;
    [SerializeField] const string behindWallLayer = "BehindWall";
    [SerializeField] const string behindWallUpLayer = "BehindWallUp";
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
        if (col.CompareTag("LimitCollider"))
        {
            IncreaseCounter();
            
            SpriteRenderer sprRenderer = col.transform.parent.GetComponentInChildren<SpriteRenderer>();

            if (behindWallSortingOrder == 2)
            {
                sprRenderer.sortingLayerName = behindWallLayer;
                sprRenderer.sortingOrder = behindWallSortingOrder;
            }
            else
            {
                sprRenderer.sortingLayerName = behindWallUpLayer;
                sprRenderer.sortingOrder = behindWallSortingOrder;
            }

        }
    }

    public void IncreaseCounter()
    {
        countOfObjects++;
        if (countOfObjects == 1 && !isTransparent)
            MakeTransparent();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("LimitCollider"))
        {
            DecreaseCounter();

            SpriteRenderer sprRenderer = col.transform.parent.GetComponentInChildren<SpriteRenderer>();

            if (overWallSortingOrder == 4)
            {
                sprRenderer.sortingLayerName = overWallLayer;
                sprRenderer.sortingOrder = overWallSortingOrder;
            }
            else
            {
                sprRenderer.sortingLayerName = overWallUpLayer;
                sprRenderer.sortingOrder = overWallSortingOrder;
            }
        }
    }

    public void DecreaseCounter()
    {
        countOfObjects--;
        if (countOfObjects == 0 && isTransparent)
            TurnOffTransparent();
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
