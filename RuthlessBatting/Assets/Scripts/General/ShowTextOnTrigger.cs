using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
    SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
        spr.enabled = false;
    }

    void OnCollisionEnter()
    {
        spr.enabled = true;
    }

    void OnCollisionExit()
    {
        spr.enabled = false;
    }
}
