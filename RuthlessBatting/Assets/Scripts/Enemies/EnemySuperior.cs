using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperior : Enemy
{
    protected override void Patrolling()
    {
        //base.Patrol();

        if (PlayerOnSight())
            OnEnemyInSight();

        if (!nav.pathPending && nav.remainingDistance < 0.5f)
            patrol.FindNextPoint();
    }

    protected override void Chasing()
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

    protected override void Attacking()
    {
        //base.Attack();

        if (!PlayerOnAttackRange())
        {
            OnEnemyOutOfAttackRange();
            return;
        }

        if (actualTime <= 0)
        {
            Debug.Log("Attacked");
            OnAttack.Invoke();
            actualTime = fireRate;
            nav.speed = 0;
            StartCoroutine(attackFSM.Attack(GetDistance()));
        }
        actualTime -= Time.deltaTime;
        
    }

    protected override void Attack()
    {
        //base.Attack();


    }
}