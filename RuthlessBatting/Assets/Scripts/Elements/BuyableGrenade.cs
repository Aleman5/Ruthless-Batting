using UnityEngine;

public class BuyableGrenade : MonoBehaviour, IBuyable
{

    [SerializeField] GameObject grenade;

    Buyable buyable = Buyable.GRENADE;
    GrenadeLauncher grScript;

    void Start ()
    {
        grScript = grenade.GetComponent<GrenadeLauncher>();
	}

    public void Buy(Transform player, int level)
    {
        if(!grenade.activeSelf)
            grenade.SetActive(true);

        grScript.SetStats(level);
    }

    public Buyable GetBuyable()
    {
        return buyable;
    }
}
