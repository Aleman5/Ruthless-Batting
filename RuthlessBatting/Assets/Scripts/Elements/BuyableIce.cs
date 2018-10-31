using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableIce : MonoBehaviour, IBuyable
{
    [SerializeField] GameObject ice;

    Buyable buyable = Buyable.GRANADE;
    Ice3D iceScript;

    void Start()
    {
        iceScript = ice.GetComponent<Ice3D>();
    }

    public void Buy(Transform player, int level)
    {
        if(!ice.activeSelf)
            ice.SetActive(true);

        iceScript.SetStats(level);
    }

    public Buyable GetBuyable()
    {
        return buyable;
    }
}
