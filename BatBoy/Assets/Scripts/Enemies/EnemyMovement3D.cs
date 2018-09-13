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
    [SerializeField] int wallLayerNumber;

    bool isMoving;
    Vector2 vecForce;
    Vector3 dir;

    Rigidbody2D rb;
    GameObject target;
    Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        isMoving = false;
        vecForce.x = 0;
        vecForce.y = speed;
    }

    void Update()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.z = 0;
        dir = diff.normalized;
        float dist = diff.magnitude;


        if (dist < rangeToHunt)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, LayerMask.NameToLayer("Wall"));
            if (hit.collider == null)
            {
                if (dist > rangeToStop)
                {
                    moveThroughPlayer();
                }
                else
                {
                    // Acá atacaría
                }
            }
            else
                Debug.Log(hit.collider.name);
        }
    }

    void moveThroughPlayer()
    {
        isMoving = true;

        //float sign = (target.transform.position.x < transform.position.x) ? -1.0f : 1.0f;

        //float angle = Vector2.Angle(target.transform.up, diff) * sign;

        //Debug.Log(angle);

        //vecForce.x = Mathf.Sin(angle)/* * diff.magnitude*/ * speed;
        //vecForce.y = Mathf.Cos(angle)/* * diff.magnitude*/ * speed;

        vecForce = dir * speed;
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
}
