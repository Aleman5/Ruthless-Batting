using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Abstract3D : MonoBehaviour
{
	

	protected float attackRate;
    protected float cooldown;

    protected int weaponLvl;
    protected int damage;

    protected virtual void Awake()
    {
		
    }

    public virtual void Attack()
    {
        Debug.Log("There is no weapon attached");
    }
}
