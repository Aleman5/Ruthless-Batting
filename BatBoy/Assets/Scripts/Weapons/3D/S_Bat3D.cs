using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bat3D : Weapons_Abstract
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	BoxCollider batBoxCollider;

    float timeToAppearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
		batBoxCollider = GetComponent<BoxCollider>();
		batBoxCollider.enabled = false;

		cooldown = 0;
        weaponLvl = 1;
        attackRate = 1;
        damage = 1;
        timeToAppearHitBox = 5 / Time.timeScale;
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

            batBoxCollider.enabled = true;

			SetBoxPreparations();

			Invoke("DesactivateBox", 0.5f); // In the future this will be the duration of the Bat Attack Animation
        }
    }

	void SetBoxPreparations()
	{
		Direction dir;

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.y = 0;
		mousePos = mousePos - transform.position;
		float angle = Vector3.SignedAngle(mousePos, transform.forward, Vector3.up);

		if		(angle >  45  && angle <   135)
			dir = Direction.RIGHT;
		else if (angle >= 135 || angle <= -135)
			dir = Direction.DOWN;
		else if (angle < -45  && angle >  -135)
			dir = Direction.LEFT;
		else
			dir = Direction.UP;

			switch(dir)
			{
			case Direction.RIGHT:
				transform.eulerAngles = new Vector3(0f, -90f, 0f);
				break;
			case Direction.DOWN:
				transform.eulerAngles = new Vector3(0f, 180f, 0f);
				break;
			case Direction.LEFT:
				transform.eulerAngles = new Vector3(0f, 90f, 0f);
				break;
			case Direction.UP:
				transform.eulerAngles = new Vector3(0f, 0f, 0);
				break;
			}
	}

	private void DesactivateBox()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

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
}