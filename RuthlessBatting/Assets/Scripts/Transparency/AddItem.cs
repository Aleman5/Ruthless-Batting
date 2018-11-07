using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
	void Start ()
    {
		if(GetComponent<SpriteRenderer>())
        {
            UpperFloorObjects.objects.Add(GetComponent<SpriteRenderer>());
        }
        else
        {
            SpriteRenderer[] sprRends = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprRend in sprRends)
                UpperFloorObjects.objects.Add(sprRend);
        }
    }
}
