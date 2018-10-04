using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    Bat script;
    Animator anim;
    float speed;

    void Start()
    {
        script = GetComponentInParent<Bat>();
        script.OnAttack.AddListener(Attacking);
        anim = GetComponent<Animator>();

        speed = anim.speed;
    }

    void Attacking()
    {
        anim.SetTrigger("Attacking");
    }

    public void SetStats(int level)
    {
        anim.speed -= speed * 0.1f * level;
    }
}
