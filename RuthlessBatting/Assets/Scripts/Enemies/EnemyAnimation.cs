using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    EnemyMovNav movScript;

    bool isChasing;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movScript = transform.parent.GetComponentInChildren<EnemyMovNav>();
        transform.parent.GetComponentInChildren<EnemyAttack>().OnAttack.AddListener(Attacking);
    }

    void Update()
    {
        anim.SetInteger("Direction", movScript.GetDirection());

        anim.speed = isChasing == false ? 0.5f : 1.0f;
    }

    void Attacking()
    {
        Debug.Log("Muerte a Shinu!");
    }
}
