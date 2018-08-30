using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bat : Weapons_Abstract
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

	BoxCollider2D batBoxCollider;

    float timeToAppearHitBox;
    float timeToDisappearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
		batBoxCollider = GetComponent<BoxCollider2D>();
		batBoxCollider.enabled = false;

		cooldown = 0;
        weaponLvl = 1;
        attackRate = 1;
        damage = 1;
        timeToAppearHitBox = 3 / Time.timeScale;
        timeToDisappearHitBox = 0.1f;
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
		mousePos.z = 0;
		mousePos = mousePos - transform.position;
		float angle = Vector3.SignedAngle(mousePos, transform.up, Vector3.forward);

		Debug.Log(angle);


		if		(angle >  45  && angle <   135)
			dir = Direction.RIGHT;
		else if (angle >= 135 && angle <= -135)
			dir = Direction.DOWN;
		else if (angle > -45  && angle <  -135)
			dir = Direction.LEFT;
		else
			dir = Direction.UP;

			switch(dir)
			{
			case Direction.RIGHT:
				batBoxCollider.offset.Set(1f, 0f);
				transform.eulerAngles = new Vector3(0f, 0f, 90f);
				break;
			case Direction.DOWN:
				batBoxCollider.offset.Set(0f, -1f);
				transform.eulerAngles = new Vector3(0f, 0f, 180f);
				break;
			case Direction.LEFT:
				batBoxCollider.offset.Set(-1f, 0f);
				transform.eulerAngles = new Vector3(0f, 0f, 270f);
				break;
			case Direction.UP:
				batBoxCollider.offset.Set(0f, 1f);
				transform.eulerAngles = new Vector3(0f, 0f, 0);
				break;
			}
	}

	private void DesactivateBox()
    {
        batBoxCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= damage;
        }
    }
}