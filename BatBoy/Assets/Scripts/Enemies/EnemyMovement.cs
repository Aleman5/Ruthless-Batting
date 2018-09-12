using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rangeToHunt;

    NavMeshAgent navMesh;
    GameObject target;
    Health health;

    void Start()
    {
        target = GameObject.Find("Player");
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.z = 0;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;


        if (dist < rangeToHunt)
        {
            if (dist > navMesh.stoppingDistance)
            {
                navMesh.destination = target.transform.position;
                
            }
            else
            {
                // Acá atacaría
            }
        }
    }

    public void Damaged(int dmg)
    {
        health.Amount -= dmg;
    }
}
