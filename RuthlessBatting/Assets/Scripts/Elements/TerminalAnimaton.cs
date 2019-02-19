using UnityEngine;

public class TerminalAnimaton : MonoBehaviour
{
    [SerializeField] Sprite interactedSprite;
    [SerializeField] Color colorToChange;
    [SerializeField] float duration;

    float t = 0.0f;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        BuyElement script = GetComponentInParent<BuyElement>();
        script.OnNotEnoughMoney.AddListener(ChangeColorSprite);
    }

    void Update()
    {
        if(spriteRenderer.color != Color.white)
        {
            Color color = Color.Lerp(spriteRenderer.color, Color.white, t);
            t += Time.deltaTime / duration;
            spriteRenderer.color = color;
            return;
        }
        t = 0.0f;
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = interactedSprite;
        spriteRenderer.color = Color.white;
    }

    void ChangeColorSprite()
    {
        spriteRenderer.color = colorToChange;
        t = 0.0f;
    }
}
