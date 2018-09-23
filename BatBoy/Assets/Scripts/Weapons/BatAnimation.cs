using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    S_Bat3D script;
    Animator anim;
    float speed;

    void Start()
    {
        script = GetComponentInParent<S_Bat3D>();
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
        GetComponent<Animation>()["Attacking"].speed = speed * 0.15f * level;
    }
}
