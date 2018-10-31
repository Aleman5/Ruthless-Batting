using UnityEngine;

public interface IBuyable
{
    void Buy(Transform player, int level);
    Buyable GetBuyable();
}
