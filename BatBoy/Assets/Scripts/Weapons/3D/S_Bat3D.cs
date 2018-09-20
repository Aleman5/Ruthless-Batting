using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Bat3D : Weapons_Abstract3D
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

    [SerializeField] PlayerMovement3D playerMovement;
	
    [SerializeField] UnityEvent onAttack;

    BoxCollider batBoxCollider;

    float timeToDisappearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
        batBoxCollider = GetComponent<BoxCollider>();
		batBoxCollider.enabled = false;

		cooldown = 1.5f;
        weaponLvl = 1;
        attackRate = 1;
        damage = 1;

        timeToDisappearHitBox = 0.5f;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (Time.time > cooldown)
        {
            cooldown = Time.time + attackRate;

            playerMovement.enabled = false;

            batBoxCollider.enabled = true;

            onAttack.Invoke();

            //SetBoxPreparations(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            Utilities.SetBoxPreparations(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            Invoke("DesactivateBox", timeToDisappearHitBox); // In the future this will be the duration of the Bat Attack Animation
        }
    }

	private void DesactivateBox()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        playerMovement.enabled = true;

        batBoxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= damage;
        }
    }

    public void SetStats(int level)
    {
        cooldown -= cooldown * 0.1f * level;
        timeToDisappearHitBox -= timeToDisappearHitBox * 0.1f * level;
    }

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }
}