using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDoubleHit : Enemy
{
    [Header("Enemy Double Hit Things")]
    [SerializeField] float timeTaunted;

    float timeLeftTaunted = 3.0f;

    protected override void Patrolling()
    {
        if (PlayerOnSight())
            OnEnemyInSight();

        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            patrol.FindNextPoint();
        }
            
    }

    protected override void Chasing()
    {
        nav.destination = player.position;
        nav.speed = speed * 2;

        if (PlayerOnAttackRange())
        {
            OnEnemyInAttackRange();
            return;
        }
    }

    protected override void Attacking()
    {
        if (!PlayerOnAttackRange())
        {
            actualTime = 0;
            OnEnemyOutOfAttackRange();
            return;
        }

        if (actualTime <= 0)
        {
            OnAttack.Invoke();
            actualTime = fireRate;
            nav.speed = 0;
            attackFSM.Attack();
        }
        actualTime -= Time.deltaTime;
        
    }

    protected override void Recovering()
    {
        if (timeLeftTaunted <= 0)
        {
            timeLeftTaunted = timeTaunted;

            OnEnemyInSight();
            return;
        }
        timeLeftTaunted -= Time.deltaTime;
    }

    protected override void Attack()
    {

    }
}