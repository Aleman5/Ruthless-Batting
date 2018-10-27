using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] GameObject bullet;

    public void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
