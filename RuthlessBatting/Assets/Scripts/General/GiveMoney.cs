using UnityEngine;

[RequireComponent(typeof(Health))]
public class GiveMoney : MonoBehaviour
{
    int moneyToGive;

    Health health;
    MoneyHolder moneyHolder;

    void Start()
    {
        moneyToGive = Random.Range(5, 9);

        health = GetComponent<Health>();
        health.OnDeath().AddListener(GiveMoneyToPlayer);

        moneyHolder = GameObject.Find("Player3D").GetComponent<MoneyHolder>();
    }

    void GiveMoneyToPlayer()
    {
        moneyHolder.ActualMoney = moneyToGive;
    }
}
