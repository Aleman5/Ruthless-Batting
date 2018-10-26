using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    protected override void Patrol()
    {
        //base.Patrol();

        if (PlayerOnSight())
            OnEnemyInSight();

        if (!nav.pathPending && nav.remainingDistance < 0.5f)
            patrol.FindNextPoint();
    }

    protected override void Chase()
    {
        //base.Chase();

        nav.destination = player.position;
        nav.speed = speed * 2;

        if (PlayerOnAttackRange())
        {
            OnEnemyInAttackRange();
            return;
        }
    }

    protected override void Attack()
    {
        //base.Attack();

        if (!PlayerOnAttackRange())
        {
            OnEnemyOutOfAttackRange();
            return;
        }

        if (actualTime <= 0)
        {
            actualTime = fireRate;
            StartCoroutine(attackFSM.Attack(GetDistance()));
        }
        actualTime -= Time.deltaTime;
        
    }
}