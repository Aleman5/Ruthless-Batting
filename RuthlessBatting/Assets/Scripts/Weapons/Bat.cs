using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bat : MonoBehaviour, IWeapon
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

    [SerializeField] PlayerMovement3D playerMovement;
    [SerializeField] BoxCollider batBoxCollider;
    [SerializeField] float horizontalAttackRange;
    [HideInInspector][SerializeField] UnityEvent onAttack;

    float cooldown;
    float attackRate;
    int weaponLvl;

    float timeToDisappearHitBox;
    float origTimeToDisappearHitBox;
    int direction;

    void Awake()
    {
        cooldown = 1.5f;
        attackRate = 0.55f;
        weaponLvl = 1;

        timeToDisappearHitBox = 0.4f;
        origTimeToDisappearHitBox = timeToDisappearHitBox;

        batBoxCollider.enabled = false;
    }

    void Update()
    {

        if (InputManager.Instance.GetFireButton() && Time.time > cooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        cooldown = Time.time + attackRate;

        //playerMovement.enabled = false;

        batBoxCollider.enabled = true;

        ActualDirection = Utilities.GetDirection(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Utilities.SetBoxPreparations(transform, ActualDirection);

        onAttack.Invoke();

        Invoke("DesactivateBox", timeToDisappearHitBox);
    }

	void DesactivateBox()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        playerMovement.enabled = true;

        batBoxCollider.enabled = false;
    }

    public void SetStats(int level)
    {
        cooldown -= cooldown * 0.1f * level;
        timeToDisappearHitBox -= origTimeToDisappearHitBox * 0.1f * level;
    }

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }

    public int ActualDirection
    {
        get { return direction; }
        set { direction = value; }
    }
}