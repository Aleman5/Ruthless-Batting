using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    [SerializeField] string objective;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(objective))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
        }
    }
}
