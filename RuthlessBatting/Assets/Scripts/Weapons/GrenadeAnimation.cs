using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAnimation : MonoBehaviour {

    Animator anim;

    void Start () {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("explode", true);
    }
}
