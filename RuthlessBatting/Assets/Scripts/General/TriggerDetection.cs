using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    [SerializeField] string objective;
    [SerializeField] LayerMask obstacules;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(objective))
        {
            Vector3 diff = collision.transform.GetChild(0).position - transform.position;
            if (!Physics.Raycast(transform.position, diff.normalized, diff.magnitude, obstacules))
            {
                Health health = collision.GetComponent<Health>();
                if (health)
                    health.Amount -= 1;
            }
        }
    }
}
