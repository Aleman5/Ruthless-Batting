using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableExtraLife : MonoBehaviour, IBuyable
{
    Buyable buyable = Buyable.EXTRAHP;

    public void Buy(Transform player, int level)
    {
        if(player.GetComponent<Health>().Amount == 1)
            player.GetComponent<Health>().Amount++;
    }

    public Buyable GetBuyable()
    {
        return buyable;
    }
}
