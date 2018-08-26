using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    [SerializeField] GameObject box;

    [SerializeField] float timeToDisappearHitBox;

    void Awake()
    {
        box.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        ActivateHitBox();
    }

    void ActivateHitBox()
    {
        box.SetActive(true);

        Invoke("DesactivateHitBox", timeToDisappearHitBox);
    }

    private void DesactivateHitBox()
    {
        box.SetActive(false);

    }
}
