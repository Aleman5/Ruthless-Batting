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

        if (PlayerOnRange())
        {
            OnEnemyInAttackRange();
            return;
        }
    }

    protected override void Attack()
    {
        //base.Attack();
        if(PlayerOnRange())
        {
            if (actualTime <= 0)
            {
                actualTime = fireRate;
                // Attack
                return;
            }
            actualTime -= Time.deltaTime;
        }
        else
        {
            OnEnemyOutOfAttackRange();
            return;
        }
    }
}