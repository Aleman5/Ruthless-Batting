using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttackDetection : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
        }
    }
}
