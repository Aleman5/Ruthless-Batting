using UnityEngine;

public class EnemyDoubleHit : Enemy
{
    [Header("Enemy Double Hit Things")]
    [SerializeField] float timeTaunted;
    [SerializeField] float forceOnHitted;

    Health health;
    float timeLeftTaunted;

    override protected void Awake()
    {
        base.Awake();

        health = GetComponent<Health>();
        health.OnHit.AddListener(OnHit);

        timeLeftTaunted = timeTaunted;
    }

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
        if(!nav.isStopped)
        {
            Vector3 dir = (transform.position - player.transform.position).normalized;
            dir.y = 0.0f;
            dir *= forceOnHitted;

            rb.drag = 10;

            rb.AddForce(dir, ForceMode.Impulse);

            nav.speed = 0;
            nav.isStopped = true;
        }

        if (timeLeftTaunted <= 0)
        {
            health.Amount += 1;
            timeLeftTaunted = timeTaunted;

            rb.drag = 20;

            nav.isStopped = false;

            OnEnemyInSight();
            return;
        }
        timeLeftTaunted -= Time.deltaTime;
    }

    protected override void Attack()
    {

    }
}