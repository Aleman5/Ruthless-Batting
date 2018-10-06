using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorRendering : MonoBehaviour {

	void Start () {    
        GetComponent<SpriteRenderer>().material.color = Color.magenta;
	}
}
