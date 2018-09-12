using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rangeToHunt;
    [SerializeField] float rangeToStop;
    [SerializeField] int wallLayerNumber;

    bool isMoving;
    Vector2 vecForce;
    Vector3 diff;

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
        diff = target.transform.position - transform.position;
        diff.z = 0;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;


        if (dist < rangeToHunt)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, dir, out hit, dist, wallLayerNumber))
            {
                if (dist > rangeToStop)
                {
                    isMoving = true;
                }
                else
                {
                    // Acá atacaría
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            float sign = (target.transform.position.x < transform.position.x) ? -1.0f : 1.0f;

            float angle = Vector2.Angle(target.transform.up, diff) * sign;

            Debug.Log(angle);

            vecForce.x = Mathf.Sin(angle) * diff.magnitude * speed;
            vecForce.y = Mathf.Cos(angle) * diff.magnitude * speed;

            rb.AddForce(vecForce);

            isMoving = false;
        }
    }

    public void Damaged(int dmg)
    {
        health.Amount -= dmg;
    }
}
