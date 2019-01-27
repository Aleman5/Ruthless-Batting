using UnityEngine;

public class EnemyAnimationBombFSM : MonoBehaviour, IAnimation
{
    [SerializeField] EnemyExplodeFSM atkScript;

    Animator anim;
    Enemy fsmScript;

    bool isChasing = false;

    void Awake()
    {
        anim      = GetComponent<Animator>();
        fsmScript = GetComponentInParent<Enemy>();
        GetComponentInParent<EnemyBase>().OnAttack.AddListener(Attacking);
        GetComponentInParent<Health>().OnDeath().AddListener(Death);
        atkScript.OnExplode().AddListener(Explode);
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

    void Explode()
    {
        anim.SetBool("ExplodeSuccess", true);
    }

    void Death()
    {
        int dir = fsmScript.GetDistance().x >= 0 ? 1 : 0;

        anim.SetInteger("Direction", dir);
        anim.SetTrigger("Death");
       

        enabled = false;
    }

    public void DisableAnim()
    {
        enabled = false;
    }
}
