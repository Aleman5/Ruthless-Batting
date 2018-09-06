using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buyZone1Text;
    [SerializeField] BuyElement buyFire1Element;

    void Start()
    {
        buyZone1Text.enabled = false;
        buyFire1Element.OnRange.AddListener(Zone1OnRange);
        buyFire1Element.OnInteract.AddListener(Zone1OnInteract);
        
    }

    void Zone1OnRange()
    {
        buyZone1Text.enabled = !buyZone1Text.enabled;
    }

    void Zone1OnInteract()
    {
        buyZone1Text.text = "Have a nice day!";
    }
}
