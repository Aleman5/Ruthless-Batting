using UnityEngine;

public class Ice3D : MonoBehaviour, IWeapon 
{
    int damage;
    float cooldown;

    void Awake() 
    {
        damage = 0;
        cooldown = 0;
    }

    void Update() 
    {
        if(Input.GetButtonDown("Fire2"))
        {
            Attack();
        }
    }

    void Attack() 
    {

    }

    public void SetStats(int level) 
    {
        switch(level)
        {
            case 1:
                damage = 1;
                cooldown = 20;
                break;
            case 2:
                damage = 1;
                cooldown = 15;
                break;
            case 3:
                damage = 1;
                cooldown = 10;
                break;
            default:
                Debug.Log("El nivel del arma no es el esperado (1, 2 o 3): " + level);
                break;
        }
    }
}
