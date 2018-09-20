using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement3D : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyToUse;
    [SerializeField] float speed;
    [SerializeField] float rangeToHunt;
    [SerializeField] float rangeToStop;
    [SerializeField] LayerMask possibleObstacules;

    bool isMoving;
    Vector3 vecForce;
    
    GameObject target;

    void Start()
    {
        target = GameObject.Find("Player_Limit");
        isMoving = false;
        vecForce = Vector3.zero;
    }

    void Update()
    {
        Vector3 diff = target.transform.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (dist < rangeToHunt)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
            {
                if (dist > rangeToStop)
                {
                    isMoving = true;
                    vecForce = dir * speed;
                }
                else
                {
                    IsAttacking = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            rigidbodyToUse.AddForce(vecForce);
            isMoving = false;
        }
    }

    public Vector3 GetDistance()
    {
        return target.transform.position - transform.position;
    }

    public bool IsAttacking { get; set; }
}
