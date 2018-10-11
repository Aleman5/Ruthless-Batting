using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovNav : MonoBehaviour {

    [Range(2, 100)]
    [SerializeField] float distToChase;
    [Range(1, 99)]
    [SerializeField] float distToStop;
    [Range(1, 6)]
    [SerializeField] float speed;
    [SerializeField] LayerMask possibleObstacules;

    Transform player;
    NavMeshAgent nav;
    Animator anim;

    void Awake()
    {
        player = GameObject.Find("Player_Limit").transform;
        anim = GetComponent<Animator>();

        nav = GetComponentInParent<NavMeshAgent>();
        nav.angularSpeed = 0;
        nav.speed = speed;
    }


    void Update()
    {
        Invoke("SetDestiny", 1.5f);
    }

    void SetDestiny()
    {
        Vector3 diff = player.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled && dist < distToChase)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
            {
                if (dist > distToStop)
                {
                    nav.SetDestination(player.position);
                    nav.speed = speed * 2;
                }
                else
                {
                    IsAttacking = true;
                }
            }
            else
            {
                nav.speed = speed;
            }
        }

        /*if (nav.enabled && (dist <= distToChase))
        {
            nav.SetDestination(player.position);
            Invoke("SetDestiny", 5f);
        }*/
    }

    public Vector3 GetDistance()
    {
        return player.position - transform.position;
    }

    public bool IsAttacking { get; set; }
    public float SetSpeed { set { nav.speed = value; } }
}
