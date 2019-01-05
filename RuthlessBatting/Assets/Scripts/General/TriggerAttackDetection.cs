using UnityEngine;

public class TriggerAttackDetection : MonoBehaviour
{
    [SerializeField] Transform originOfTheRay;
    [SerializeField] LayerMask obstacules;
    [SerializeField] string[] tagObjective;

    Health healthScript;

    void Awake()
    {
        healthScript = GetComponentInParent<Health>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(tagObjective[0]) && healthScript.Amount > 0)
        {
            Vector3 diff = collision.transform.GetChild(0).position - originOfTheRay.position;
            if (!Physics.Raycast(originOfTheRay.position, diff.normalized, diff.magnitude, obstacules))
            {
                Health health = collision.GetComponent<Health>();
                health.Amount -= 1;

                AudioManager.Instance.RunAudio(Audios.matar_enemigo_bath);
            }
        }

        if (collision.CompareTag(tagObjective[1]))
            AudioManager.Instance.RunAudio(Audios.bate_pared);
    }
}
