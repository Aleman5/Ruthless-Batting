using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableFire : MonoBehaviour, IBuyable
{

    [SerializeField] GameObject fire;

    Fire fireScript;

    void Start ()
    {
        fireScript = fire.GetComponent<Fire>();
	}

    public void Buy(Transform player, int level)
    {
        if(!fire.activeSelf)
            fire.SetActive(true);

        fireScript.SetStats(level);
    }
}
