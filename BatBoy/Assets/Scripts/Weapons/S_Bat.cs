using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bat : Weapons_Abstract
{
    BoxCollider2D batBoxCollider;

    float timeToAppearHitBox;
    float timeToDisappearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
        cooldown = 0;
        weaponLvl = 1;
        attackRate = 1;
        damage = 1;
        timeToAppearHitBox = 3 / Time.timeScale;
        timeToDisappearHitBox = 0.1f;

        batBoxCollider = GetComponent<BoxCollider2D>();
        batBoxCollider.enabled = false;
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

            Invoke("DesactivateBox", 0.5f); // In the future this will be the duration of the Bat Attack
        }
    }

    private void DesactivateBox()
    {
        batBoxCollider.enabled = false;

        Debug.Log("Me estoy desactivando perro");
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