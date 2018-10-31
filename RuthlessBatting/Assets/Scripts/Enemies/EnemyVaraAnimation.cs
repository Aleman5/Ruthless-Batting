using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVaraAnimation : MonoBehaviour
{
    EnemyBase script;
    Animator anim;

    void Start()
    {
        script = transform.parent.GetComponentInParent<EnemyBase>();
        script.OnAttack.AddListener(Attacking);
        anim = GetComponent<Animator>();
        anim.speed = 1.20f;
    }

    void Attacking()
    {
        anim.SetTrigger("Attacking");
    }
}
