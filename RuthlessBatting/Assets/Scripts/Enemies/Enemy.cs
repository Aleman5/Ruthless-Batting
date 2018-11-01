using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    public enum States
    {
        Patrol = 0,
        Chase,
        Attack,
        Recovery,
        Death,
        Count
    }

    public enum Events
    {
        InSight = 0,
        OutOfSight,
        InAttackRange,
        OutOfAttackRange,
        OnHit,
        NoHealth,
        Count
    }

    EnemyFSM fsm;

    // ===========================================================
    // Inicialización
    // ===========================================================
    private void Start()
    {
        fsm = new EnemyFSM((int)States.Count, (int)Events.Count, (int)States.Patrol);

                                  // Origin             // Event                      // Destiny
        fsm.SetRelation( (int)States.Patrol,   (int)Events.InSight,           (int)States.Chase    );
        fsm.SetRelation( (int)States.Recovery, (int)Events.InSight,           (int)States.Chase    );
        fsm.SetRelation( (int)States.Chase,    (int)Events.InAttackRange,     (int)States.Attack   );
        fsm.SetRelation( (int)States.Chase,    (int)Events.OutOfSight,        (int)States.Patrol   );
        fsm.SetRelation( (int)States.Attack,   (int)Events.OutOfAttackRange,  (int)States.Chase    );
        fsm.SetRelation( (int)States.Patrol,   (int)Events.OnHit,             (int)States.Recovery );
        fsm.SetRelation( (int)States.Chase,    (int)Events.OnHit,             (int)States.Recovery );
        fsm.SetRelation( (int)States.Attack,   (int)Events.OnHit,             (int)States.Recovery );
        fsm.SetRelation( (int)States.Patrol,   (int)Events.NoHealth,          (int)States.Death    );
        fsm.SetRelation( (int)States.Chase,    (int)Events.NoHealth,          (int)States.Death    );
        fsm.SetRelation( (int)States.Attack,   (int)Events.NoHealth,          (int)States.Death    );
        fsm.SetRelation( (int)States.Recovery, (int)Events.NoHealth,          (int)States.Death    );
    }

    // ===========================================================
    // Métodos virtuales
    // ===========================================================
    override protected void OnUpdate()
    {
        Debug.Log((States)fsm.GetState());

        switch (fsm.GetState())
        {
            case (int)States.Patrol:
                Patrolling();
                break;
            case (int)States.Chase:
                Chasing();
                break;
            case (int)States.Attack:
                Attacking();
                break;
            case (int)States.Recovery:
                Recovering();
                break;
            case (int)States.Death:

                break;
        }
    }

    virtual protected void Patrolling()
    {

    }

    virtual protected void Chasing()
    {

    }

    virtual protected void Attacking()
    {

    }

    virtual protected void Recovering()
    {

    }

    virtual protected void Attack()
    {

    }

    // ===========================================================
    // Eventos
    // ===========================================================

    override protected void OnEnemyInSight()
    {
        fsm.SendEvent((int)Events.InSight);
    }

    override protected void OnEnemyOutOfSight()
    {
        fsm.SendEvent((int)Events.OutOfSight);
    }

    override protected void OnEnemyInAttackRange()
    {
        fsm.SendEvent((int)Events.InAttackRange);
    }

    override protected void OnEnemyOutOfAttackRange()
    {
        fsm.SendEvent((int)Events.OutOfAttackRange);
    }

    protected override void OnHit()
    {
        fsm.SendEvent((int)Events.OnHit);
    }

    protected override void OnNoHealth()
    {
        fsm.SendEvent((int)Events.NoHealth);
    }

    public int GetActualState()
    {
        return fsm.GetState();
    }
}