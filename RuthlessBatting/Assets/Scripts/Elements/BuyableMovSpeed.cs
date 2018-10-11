using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableMovSpeed : MonoBehaviour, IBuyable {

    [SerializeField] PlayerMovement3D movementToImprove;

    public void Buy(Transform player, int level)
    {
        movementToImprove.SetStats(level);
    }
}
