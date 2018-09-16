using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimation : MonoBehaviour
{
    S_Bat3D script;
    Animator anim;

    void Start()
    {
        script = GetComponentInParent<S_Bat3D>();
        script.OnAttack.AddListener(Attacking);
        anim = GetComponent<Animator>();
    }

    void Attacking()
    {
        anim.SetTrigger("Attacking");
    }
}
