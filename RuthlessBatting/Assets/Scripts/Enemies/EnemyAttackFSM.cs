using System.Collections;
using UnityEngine;

public class EnemyAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] Enemy fsmScript;
    [SerializeField] BoxCollider box;
    [SerializeField] float timeToAttack;

    [SerializeField] Transform originOfTheRay;
    [SerializeField] LayerMask obstacules;
    [SerializeField] string tagObjective;

    void Awake()
    {
        box.enabled = false;
    }

    public void Attack()
    {
        StartCoroutine("AttackCourutine");
    }

    IEnumerator AttackCourutine()
    {
        Vector3 dist = fsmScript.GetDistance();
        dist.y = 0;
        transform.rotation = Quaternion.LookRotation(dist, Vector3.up);

        fsmScript.enabled = false;

        AudioManager.Instance.RunAudio(Audios.cachiporrazo_electrico);

        ChangeBoxState();

        Utilities.GetDirection(transform, dist);

        yield return new WaitForSeconds(0.4f);
        
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ChangeBoxState();

        yield return new WaitForSeconds(0.5f);

        fsmScript.enabled = true;

        yield break;
    }

    private void ChangeBoxState()
    {
        box.enabled = !box.enabled;
    }
}
