using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject target;

    [SerializeField] float speed;
    [SerializeField] float distanceMin;
    [SerializeField] float distanceMax;

    Health health;

    void Awake()
    {
        target = GameObject.Find("Player");
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 diff = target.transform.position - transform.position;
        diff.z = 0;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;


        if (dist < distanceMax)
        {
            if (dist > distanceMin)
            {
                Vector2 vecForce;
                vecForce.x = Input.GetAxis("Horizontal") * speed;
                vecForce.y = Input.GetAxis("Vertical") * speed;
                rb.AddForce(vecForce);
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
