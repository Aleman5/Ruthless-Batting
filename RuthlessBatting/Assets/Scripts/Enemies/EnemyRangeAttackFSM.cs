using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] GameObject bullet;

    EnemyShooter scriptFSM;

    public void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
