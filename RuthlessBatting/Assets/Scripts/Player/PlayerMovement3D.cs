using UnityEngine;
using System.IO;

public class PlayerMovement3D : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyToUse;
    [SerializeField] float speed;
    [SerializeField] float speedOnStairs;
    [SerializeField] float dashForce;

    int upgMovLevel; // Upgrade level
    float angleOfTheStairs = 36.0f; // Angle of the Stairs.
    float originalMovSpeed;
    float dashCooldownTime = 0.6f;
    float dashCooldown;
    bool isOnStairs = false;
    Vector3 movForce;
    Vector3 stairsDir;

    const string overBloodLayer = "OverBlood";
    const string overBloodUpLayer = "OverBloodUp";
    int overBloodSortingOrder = 3;

    void Start()
    {
        Health health = GetComponent<Health>();
        health.OnDeath().AddListener(Death);

        originalMovSpeed = speed;

        if (File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            int[] upgValues = SaveLoad.saveGame.data.playerUpgrades;
            
            if (upgValues[(int)Buyable.MOVSPEED] > 0) SetStats(upgValues[(int)Buyable.MOVSPEED]);
            if (upgValues[(int)Buyable.ATKSPEED] > 0) GetComponentInChildren<Bat>().SetStats(upgValues[(int)Buyable.ATKSPEED]);
            if (upgValues[(int)Buyable.GRENADE ] > 0) GetComponentInChildren<GrenadeLauncher>().SetStats(upgValues[(int)Buyable.GRENADE]);
            if (upgValues[(int)Buyable.EXTRAHP ] > 0) health.Amount = upgValues[(int)Buyable.EXTRAHP];
        }

        stairsDir.x = 0.0f;
        stairsDir.y = 0.587785f;
        stairsDir.z = 0.809017f;

        dashCooldown = dashCooldownTime;
    }

    void Update()
    {
        dashCooldown -= Time.deltaTime;

        movForce = Vector3.zero;

        movForce.x = InputManager.Instance.GetHorizontalAxis();
        movForce.z = InputManager.Instance.GetVerticalAxis();

        if (!isOnStairs)
        {
            movForce.x *= speed;
            movForce.z *= speed;
            movForce.y = 0;

            if (InputManager.Instance.GetDashButton() && dashCooldown < 0)
            {
                dashCooldown = dashCooldownTime;
                MakeForceMovement();
            }
        }
        else
        {
            movForce.z *= stairsDir.z;

            movForce.y = movForce.z * stairsDir.y;

            movForce = movForce.normalized * speedOnStairs;
        }

        // Para Testing nomás
        if (Input.GetKey(KeyCode.H))
        {
            GetComponent<Health>().Amount++;
        }
    }

    void FixedUpdate()
    {
        rigidbodyToUse.AddForce(movForce);
    }

    public void MakeForceMovement()
    {
        rigidbodyToUse.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Vector3 newForce = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        newForce.y = 0;
        newForce = newForce.normalized * dashForce;

        rigidbodyToUse.AddForce(newForce);
    }

    public int GetUpgradeValue(int index)
    {
        switch (index)
        {
            case (int)Buyable.MOVSPEED:
                return GetUpgradeValue();
            case (int)Buyable.ATKSPEED:
                return GetComponentInChildren<Bat>().GetUpgradeValue();
            case (int)Buyable.GRENADE:
                return GetComponentInChildren<GrenadeLauncher>().GetUpgradeValue();
            case (int)Buyable.EXTRAHP:
                return (int)GetComponent<Health>().Amount;
        }

        return -1;
    }

    void Death()
    {
        Transform t1 = transform.GetChild(1);
        
        SpriteRenderer sR = t1.GetComponent<SpriteRenderer>();
        if (sR.sortingLayerName != "OverWallUp" && sR.sortingLayerName != "BehindWallUp")
        {
            sR.sortingLayerName = overBloodLayer;
            sR.sortingOrder = overBloodSortingOrder;
        }
        else
        {
            sR.sortingLayerName = overBloodUpLayer;
            sR.sortingOrder = overBloodSortingOrder;
        }

        PlayerAnimations anim = t1.GetComponent<PlayerAnimations>();
        anim.Death();

        t1.parent = null;

        Destroy(gameObject);
    }

    public void SetStats(int level)
    {
        upgMovLevel = level;

        speed = originalMovSpeed + originalMovSpeed * (0.05f * level);
    }

    public int GetUpgradeValue() { return upgMovLevel; }

    public bool IsOnStairs { set { isOnStairs = value; }  }
}
