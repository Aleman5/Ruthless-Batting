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
    [SerializeField] BoxCollider batBoxCollider;
    [SerializeField] float horizontalAttackRange;
    [SerializeField] float dashForce;
    [SerializeField] UnityEvent onAttack;

    float timeToDisappearHitBox;
    float timeBtwChange;
    float distanceOfBox;
    float distanceToMovePerFrame;
    int amountOfFrames;

    int directionOfTheAttack;

    Vector3 oriPosBox;

    protected override void Awake()
    {
        //batBoxCollider = GetComponent<BoxCollider>();

        //oriPosBox = batBoxCollider.transform.position;

        cooldown = 1.5f;
        weaponLvl = 1;
        attackRate = 1;
        damage = 1;

        amountOfFrames = 6;

        timeToDisappearHitBox = 0.42f;
        timeBtwChange = timeToDisappearHitBox / amountOfFrames;

        batBoxCollider.size.Set(horizontalAttackRange / amountOfFrames,
                                0.5f, 
                                0.6f);
        batBoxCollider.center.Set(horizontalAttackRange / 2,
                                  0,
                                  1.106f);
        distanceToMovePerFrame = batBoxCollider.size.x;

        batBoxCollider.enabled = false;
    }

    public void Update()
    {
        if (InputManager.Instance.GetFireButton() && Time.time > cooldown)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        StartCoroutine(Attacking());

        /*if (Time.time > cooldown)
        {
            cooldown = Time.time + attackRate;

            playerMovement.enabled = false;

            batBoxCollider.enabled = true;

            onAttack.Invoke();

            Utilities.SetBoxPreparations(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            Invoke("DesactivateBox", timeToDisappearHitBox); // In the future this will be the duration of the Bat Attack Animation
        }*/
    }

    IEnumerator Attacking()
    {
        cooldown = Time.time + attackRate;

        playerMovement.enabled = false;

        batBoxCollider.enabled = true;

        DirectionOfTheAttack = Utilities.SetBoxPreparations(transform, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        onAttack.Invoke();

        switch (DirectionOfTheAttack)
        {
            case 0:
                for (int i = 0; i < amountOfFrames + 1; i++)
                {
                    Vector3 vecMove = Vector3.zero;
                    vecMove.x = distanceToMovePerFrame;
                    batBoxCollider.transform.position += -vecMove;
                    playerMovement.MakeForceMovement(Vector3.forward * dashForce);

                    yield return new WaitForSeconds(timeBtwChange);
                }
                break;
            case 1:
                for (int i = 0; i < amountOfFrames + 1; i++)
                {
                    Vector3 vecMove = Vector3.zero;
                    vecMove.z = distanceToMovePerFrame;
                    batBoxCollider.transform.position += vecMove;
                    playerMovement.MakeForceMovement(Vector3.right * dashForce);

                    yield return new WaitForSeconds(timeBtwChange);
                }
                break;
            case 2:
                for (int i = 0; i < amountOfFrames + 1; i++)
                {
                    Vector3 vecMove = Vector3.zero;
                    vecMove.x = distanceToMovePerFrame;
                    batBoxCollider.transform.position += vecMove;
                    playerMovement.MakeForceMovement(-Vector3.forward * dashForce);

                    yield return new WaitForSeconds(timeBtwChange);
                }
                break;
            case 3:
                for (int i = 0; i < amountOfFrames + 1; i++)
                {
                    Vector3 vecMove = Vector3.zero;
                    vecMove.z = distanceToMovePerFrame;
                    batBoxCollider.transform.position += -vecMove;
                    playerMovement.MakeForceMovement(-Vector3.right * dashForce);

                    yield return new WaitForSeconds(timeBtwChange);
                }
                break;
        }

        DesactivateBox();

        yield break;
    }

	private void DesactivateBox()
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        playerMovement.enabled = true;

        batBoxCollider.transform.position = oriPosBox + playerMovement.transform.position;

        batBoxCollider.enabled = false;
    }

    /*void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= damage;
        }
    }*/

    public void SetStats(int level)
    {
        cooldown -= cooldown * 0.1f * level;
        timeToDisappearHitBox -= timeToDisappearHitBox * 0.1f * level;
        timeBtwChange = timeToDisappearHitBox / amountOfFrames;
    }

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }

    public int DirectionOfTheAttack
    {
        get { return directionOfTheAttack; }
        set { directionOfTheAttack = value; }
    }
}