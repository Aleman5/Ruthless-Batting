using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperFloorObjects : MonoBehaviour
{
    static public List<SpriteRenderer> objects;

    static public void ChangeTransparency(float percentage)
    {
        foreach (SpriteRenderer obj in objects)
        {
            Color color = obj.material.color;
            color.a = percentage;
            obj.material.color = color;
        }
    }
}
