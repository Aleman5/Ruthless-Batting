using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttackDetection : MonoBehaviour
{
    [SerializeField] Transform originOfTheRay;
    [SerializeField] LayerMask obstacules;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3 diff = collision.transform.position - originOfTheRay.position;
            if (!Physics.Raycast(originOfTheRay.position, diff.normalized, diff.magnitude, obstacules))
            {
                Health health = collision.GetComponent<Health>();
                health.Amount -= 1;
            }
        }
    }
}
