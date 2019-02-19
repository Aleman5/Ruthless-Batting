public class EnemyBomb : Enemy
{
    protected override void Patrolling()
    {
        //base.Patrol();

        if (player && PlayerOnSight())
            OnEnemyInSight();

        if (!nav.pathPending && nav.remainingDistance < 0.5f)
        {
            patrol.FindNextPoint();
        }
            
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
        OnAttack.Invoke();
        actualTime = fireRate;
        nav.speed = 0;
        attackFSM.Attack();
    }

    protected override void Attack()
    {
        //base.Attack();


    }
}