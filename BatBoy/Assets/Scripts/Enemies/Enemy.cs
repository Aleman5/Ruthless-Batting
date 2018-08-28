using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float distanceMin;
    [SerializeField] float distanceMax;

    Health health;

    void Awake()
    {
        
    }

    void Update()
    {
        Vector3 diff = target.position - transform.position;
        diff.z = 0;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;
        

        if (dist < distanceMax)
        {
            if(dist > distanceMin)
            {
                transform.position += dir * speed * Time.deltaTime;
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
