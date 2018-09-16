using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableAttackSpeed : MonoBehaviour, IBuyable
{
    public void Buy(Transform player, int level)
    {
        player.GetComponent<S_Bat3D>().SetStats(level);
        player.GetComponent<BatAnimation>().SetStats(level);
    }
}
