using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] S_Bat3D batScript;

	Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
        batScript.OnAttack.AddListener(SetAttackTrigger);
	}

	void Update() 
	{
		anim.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
		anim.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
	}

    void SetAttackTrigger()
    {
        anim.SetTrigger("Attacking");
        anim.SetInteger("Direction", batScript.DirectionOfTheAttack);
    }
}
