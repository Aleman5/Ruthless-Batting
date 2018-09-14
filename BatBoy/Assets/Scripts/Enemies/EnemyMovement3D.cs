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

    Rigidbody rb;
    GameObject target;
    Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player3D");
        isMoving = false;
        vecForce = Vector2.zero;
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

                    //moveThroughPlayer();
                }
                else
                {
                    // Acá atacaría
                }
            }
        }
    }

    /*void moveThroughPlayer()
    {
        isMoving = true;

        float sign = (target.transform.position.x < transform.position.x) ? -1.0f : 1.0f;

        float angle = Vector2.Angle(target.transform.up, diff) * sign;

        Debug.Log(angle);

        vecForce.x = Mathf.Sin(angle) * speed;
        vecForce.y = Mathf.Cos(angle) * speed;

        vecForce = dir * speed;
    }*/

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
}
