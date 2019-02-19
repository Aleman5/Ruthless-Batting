using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Bat batScript;

    Animator anim;

    private void Awake() {
		anim = GetComponent<Animator>();
        batScript.OnAttack.AddListener(SetAttackTrigger);
	}

	void Update() 
	{
        anim.SetFloat("VerticalSpeed", InputManager.Instance.GetVerticalAxis());
        anim.SetFloat("HorizontalSpeed", InputManager.Instance.GetHorizontalAxis());
	}

    void SetAttackTrigger()
    {
        anim.SetTrigger("Attacking");
        anim.SetInteger("Direction", batScript.ActualDirection);
    }

    public void Death()
    {
        anim.SetTrigger("Death");

        enabled = false;
    }
}
