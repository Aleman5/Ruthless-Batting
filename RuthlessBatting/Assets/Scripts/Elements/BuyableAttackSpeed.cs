using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableAttackSpeed : MonoBehaviour, IBuyable
{
    [SerializeField] Transform bat;

    public void Buy(Transform player, int level)
    {
        if(bat.gameObject.activeSelf)
        {
            bat.GetComponent<Bat>().SetStats(level);
            bat.GetComponentInChildren<BatAnimation>().SetStats(level);
        }
    }
}
