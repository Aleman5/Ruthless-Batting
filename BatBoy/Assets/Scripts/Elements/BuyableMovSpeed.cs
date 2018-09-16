using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableMovSpeed : MonoBehaviour, IBuyable {

    public void Buy(Transform player, int level)
    {
        player.GetComponent<PlayerMovement3D>().SetStats(level);
    }
}
