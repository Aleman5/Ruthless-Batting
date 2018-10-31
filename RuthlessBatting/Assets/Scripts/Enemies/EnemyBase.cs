using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Variables")]
    [Range(2, 20)]
    public float distToChase;
    [Range(1, 15)]
    public float distToStop;
    [Range(1, 7)]
    public float speed;
    [Range(0.5f, 4.0f)]
    public float fireRate;
    public LayerMask possibleObstacules;

    protected bool drawGizmos = true;

    [HideInInspector] public IPatrol patrol;
    [HideInInspector] public IAttack attackFSM;
    [HideInInspector] public Transform player;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public float actualTime = 0.0f;

    [HideInInspector][SerializeField] UnityEvent onAttack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;

        nav = GetComponent<NavMeshAgent>();
        nav.angularSpeed = 0;
        nav.speed = speed;

        attackFSM = GetComponentInChildren<IAttack>();
        patrol    = GetComponent<IPatrol>();
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
        Vector3 diff = GetDistance();
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled)
        {
            RaycastHit hit;

            if (dist < distToChase)
                if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
                    return true;
        }

        return false;
    }

    public bool PlayerOnAttackRange()
    {
        Vector3 diff = GetDistance();
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled)
        {
            RaycastHit hit;

            if (dist < distToStop)
                if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
                    return true;
        }

        return false;
    }

    public Vector3 GetDistance()
    {
        return player.transform.position - transform.position;
    }

    public int GetDirection()
    {
        return Utilities.GetDirection(transform, nav.destination - nav.transform.position);
    }

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }

    /*private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distToStop);

            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, distToChase);
        }
    }*/
}