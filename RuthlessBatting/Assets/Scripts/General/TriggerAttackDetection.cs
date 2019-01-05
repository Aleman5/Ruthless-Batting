using UnityEngine;

public class TriggerAttackDetection : MonoBehaviour
{
    Health healthScript;
    [SerializeField] Transform originOfTheRay;
    [SerializeField] LayerMask obstacules;
    [SerializeField] string tagObjective;

    void Awake()
    {
        healthScript = GetComponentInParent<Health>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(tagObjective) && healthScript.Amount > 0)
        {
            Vector3 diff = collision.transform.GetChild(0).position - originOfTheRay.position;
            if (!Physics.Raycast(originOfTheRay.position, diff.normalized, diff.magnitude, obstacules))
            {
                Health health = collision.GetComponent<Health>();
                health.Amount -= 1;

                AudioManager.Instance.RunAudio(Audios.matar_enemigo_bath);
            }
        }
    }
}
