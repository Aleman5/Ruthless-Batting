using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public class EnemyMovement3D : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rangeToHunt;
    [SerializeField] float rangeToStop;
    [SerializeField] LayerMask possibleObstacules;

    bool isMoving;
    Vector3 vecForce;
    Vector3 vecFromOrigToBottom;

    Rigidbody rb;
    GameObject target;
    Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player3D");
        isMoving = false;
        vecForce = Vector3.zero;

        vecFromOrigToBottom.x = 0f;
        vecFromOrigToBottom.y = -0.5f;
        vecFromOrigToBottom.z = 0f;
    }

    void Update()
    {
        Vector3 diff = target.transform.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (dist < rangeToHunt)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position - vecFromOrigToBottom, dir, out hit, dist, possibleObstacules))
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
            rb.AddForce(vecForce);
            isMoving = false;
        }
    }

    public void Damaged(int dmg)
    {
        health.Amount -= dmg;
    }

    public Vector3 GetDistance()
    {
        return target.transform.position - transform.position;
    }

    public bool IsAttacking { get; set; }
}
