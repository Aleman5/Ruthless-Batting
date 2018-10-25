using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationFSM : MonoBehaviour
{
    Animator anim;
    Enemy fsmScript;

    bool isChasing;

    void Awake()
    {
        anim = GetComponent<Animator>();
        fsmScript = transform.parent.GetComponentInChildren<Enemy>();
        transform.parent.GetComponentInChildren<EnemyAttackFSM>().OnAttack.AddListener(Attacking);
    }

    void Update()
    {
        anim.SetInteger("Direction", fsmScript.GetDirection());

        anim.speed = isChasing == false ? 0.5f : 1.0f;
    }

    void Attacking()
    {
        Debug.Log("Muerte a Shinu!");
    }
}
