using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bat : Weapons_Abstract
{
    BoxCollider2D box;

    float timeToAppearHitBox;
    float timeToDisappearHitBox;
    float distanceOfBox;

    protected override void Awake()
    {
        base.Awake();
        attackRate = 1;
        timeToAppearHitBox = 3 / Time.timeScale;
        timeToDisappearHitBox = 0.1f;

        box = GetComponent<BoxCollider2D>();
        box.size.Set(1.75f, 1f);
        box.offset.Set(0f, 1f);
        box.enabled = false;

    }

    public override void Attack()
    {
        if(Time.time > timeToAttack)
        {
            timeToAttack = Time.time + attackRate;

            //Invoke("ActivateHitBox", timeToAppearHitBox);
            ActivateHitBox();

            Debug.Log("Me estoy yendo perro");
        }
    }

    void ActivateHitBox()
    {
        box.enabled = true;

        Debug.Log("Me estoy activando perro");

        Invoke("DesactivateHitBox", timeToDisappearHitBox);
    }

    private void DesactivateHitBox()
    {
        box.enabled = false;

        Debug.Log("Me estoy desactivando perro");
    }
}