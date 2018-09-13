using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableIce : MonoBehaviour, IBuyable
{
    [SerializeField] GameObject ice;

    Ice iceScript;

    void Start()
    {
        iceScript = ice.GetComponent<Ice>();
    }

    public void Buy(Transform player, int level)
    {
        if(!ice.activeSelf)
            ice.SetActive(true);

        iceScript.SetStats(level);
    }
}
