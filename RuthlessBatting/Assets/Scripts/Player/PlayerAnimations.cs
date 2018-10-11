using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Bat batScript;

    float idleFloat;

	Animator anim;

    /* TENER UNA VARIABLE QUE SE GUARDE EL ULTIMO VALOR DEL FLOAT QUE USA EL B.T (VERIFICANDO QUE NO SEA 0) */
    /* PARA PODER DECIDIR QUE IDLE UTILIZAR.                                                                */

    private void Awake() {
		anim = GetComponent<Animator>();
        batScript.OnAttack.AddListener(SetAttackTrigger);
	}

	void Update() 
	{
		anim.SetFloat("VerticalSpeed", InputManager.Instance.GetVerticalAxis());
		anim.SetFloat("HorizontalSpeed", InputManager.Instance.GetHorizontalAxis());
        //idleFloat = anim.GetFloat("")
	}

    void SetAttackTrigger()
    {
        anim.SetTrigger("Attacking");
        anim.SetInteger("Direction", batScript.DirectionOfTheAttack);
    }
}
