using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Bat3D : Weapons_Abstract
{
	enum Direction
	{
		UP,
		DOWN,
		RIGHT,
		LEFT
	}

    PlayerMovement3D playerMovement;
	BoxCollider batBoxCollider;
    [SerializeField] UnityEvent onAttack;

    float timeToAppearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement3D>();

        batBoxCollider = GetComponent<BoxCollider>();
		batBoxCollider.enabled = false;

		cooldown = 1.5f;
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

            playerMovement.enabled = false;

            batBoxCollider.enabled = true;

            onAttack.Invoke();

			SetBoxPreparations(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

			Invoke("DesactivateBox", 0.48f); // In the future this will be the duration of the Bat Attack Animation
        }
    }

	void SetBoxPreparations(Vector3 distance)
	{
        distance.y = 0;
		float angle = Vector3.SignedAngle(distance, transform.forward, Vector3.up);

		if		(angle >  45  && angle <   135)
            transform.eulerAngles = new Vector3(0f, -90f, 0f);
        else if (angle >= 135 || angle <= -135)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        else if (angle < -45  && angle >  -135)
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0);
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

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }
}