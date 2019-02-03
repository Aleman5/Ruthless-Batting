using UnityEngine;

public class EnemyAnimationFSM : MonoBehaviour, IAnimation
{
    [SerializeField] float mountOfPossibleDeaths = 2;

    Animator anim;
    Enemy fsmScript;

    bool isChasing = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        fsmScript = transform.GetComponentInParent<Enemy>();
        transform.GetComponentInParent<EnemyBase>().OnAttack.AddListener(Attacking);
    }

    void Update()
    {
        int dir = fsmScript.GetDestinationDistance().x >= 0 ? 1 : 0;

        anim.SetInteger("Direction", dir);

        anim.speed = isChasing == false ? 0.5f : 1.0f;

        if (fsmScript.PlayerOnSight())
            anim.speed = 1.6f;
        else if (fsmScript.PlayerOnAttackRange())
            anim.speed = 1f;
        
    }

    void Attacking()
    {
        anim.SetTrigger("Attack");
    }

    public void Death()
    {
        int dir = fsmScript.GetDistance().x >= 0 ? 1 : 0;
        float whichDeath = Random.Range(0, mountOfPossibleDeaths);

        if (whichDeath == 0)
            anim.SetFloat("WhichDeath", 0.0f);
        else
        {
            float f = whichDeath / mountOfPossibleDeaths;
            anim.SetFloat("WhichDeath", f);
        }
        anim.SetInteger("Direction", dir);
        anim.SetTrigger("Death");

        enabled = false;
    }

    public void DisableAnim()
    {
        enabled = false;
    }
}
