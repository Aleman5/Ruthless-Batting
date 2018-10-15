using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] EnemyMovNav enemyMovement;
    private float rate;
    [SerializeField] float startRate;
    [SerializeField] GameObject bullet;

    void Awake()
    {
        rate = startRate;
    }

    void Update()
    {
        if (enemyMovement.IsAttacking)
        {
            if (rate <= 0)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                rate = startRate;
            }
            else
            {
                rate -= Time.deltaTime;
            }
        }
    }
}
