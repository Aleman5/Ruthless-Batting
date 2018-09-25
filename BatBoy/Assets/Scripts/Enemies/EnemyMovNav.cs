using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovNav : MonoBehaviour {

    [SerializeField] float distToChase = 50;
    private Transform player;
    private NavMeshAgent nav;

    Animator anim;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        nav.angularSpeed = 0;
    }


    void Update()
    {
            Invoke("SetDestiny", 1f);
    }

    void SetDestiny()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (nav.enabled && (dist <= distToChase))
        {
            nav.SetDestination(player.position);
            Invoke("SetDestiny", 5f);
        }
    }
}
