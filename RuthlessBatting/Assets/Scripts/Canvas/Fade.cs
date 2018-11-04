using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    [SerializeField] float fadeRate;
    private Image image;
    private float difference = 0.1f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        FadeIn();
    }

    void FadeIn()
    {
            fadeRate += difference * Time.deltaTime;
            if (fadeRate < 0)
                fadeRate = 0;
            image.color = new Color(255, 255, 255, fadeRate);
    }
}