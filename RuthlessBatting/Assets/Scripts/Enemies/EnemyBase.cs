using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Range(2, 20)]
    public float distToChase;
    [Range(1, 15)]
    public float distToStop;
    [Range(1, 7)]
    public float speed;
    public float fireRate;
    public LayerMask possibleObstacules;

    protected bool drawGizmos = true;

    [HideInInspector] public Transform player;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public PatrolFSM patrol;
    Animator anim;

    void Awake()
    {
        player = GameObject.Find("Player_Limit").transform;
        anim = GetComponent<Animator>();

        nav = GetComponentInParent<NavMeshAgent>();
        nav.angularSpeed = 0;
        nav.speed = speed;

        patrol = GetComponent<PatrolFSM>();
    }

    protected void Update()
    {
        OnUpdate();
    }

    protected void FixedUpdate()
    {
        
    }

    public void EnemyInSight()
    {

    }

    public void EnemyInAttackRange()
    {
        
    }

    abstract protected void OnUpdate();

    virtual protected void OnEnemyInSight()
    {

    }
    virtual protected void OnEnemyOutOfSight()
    {

    }
    virtual protected void OnEnemyInAttackRange()
    {

    }
    virtual protected void OnEnemyOutOfAttackRange()
    {

    }
    virtual protected void OnNoHealth()
    {

    }

    public bool PlayerOnSight()
    {
        Vector3 diff = player.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
                return true;
            /*{
                

                if (dist > distToStop)
                {
                    if (!isChasing) isChasing = true;
                    nav.SetDestination(player.position);
                    nav.speed = speed * 2;
                }
                else
                {
                    if (isChasing) isChasing = false;
                    IsAttacking = true;
                }
            }
            else
            {
                if (isChasing) isChasing = false;
                nav.speed = speed;
            }*/
        }

        return false;
    }

    public bool PlayerOnRange()
    {
        Vector3 diff = player.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
            {
                if (dist < distToStop)
                {
                    return true;
                }
            }
            /*{
                

                if (dist > distToStop)
                {
                    if (!isChasing) isChasing = true;
                    nav.SetDestination(player.position);
                    nav.speed = speed * 2;
                }
                else
                {
                    if (isChasing) isChasing = false;
                    IsAttacking = true;
                }
            }
            else
            {
                if (isChasing) isChasing = false;
                nav.speed = speed;
            }*/
        }

        return false;
    }

    public int GetDirection()
    {
        return Utilities.GetDirection(transform, nav.destination - nav.transform.position);
    }

    public Vector3 GetDistance()
    {
        return player.position - transform.position;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distToStop);

            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distToChase);
        }
    }
}