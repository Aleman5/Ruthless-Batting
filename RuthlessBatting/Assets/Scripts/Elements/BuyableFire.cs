using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableFire : MonoBehaviour, IBuyable
{

    [SerializeField] GameObject fire;

    Buyable buyable = Buyable.GRANADE;
    Fire3D fireScript;

    void Start ()
    {
        fireScript = fire.GetComponent<Fire3D>();
	}

    public void Buy(Transform player, int level)
    {
        if(!fire.activeSelf)
            fire.SetActive(true);

        fireScript.SetStats(level);
    }

    public Buyable GetBuyable()
    {
        return buyable;
    }
}
