using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSlowDown : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent.CompareTag("Player"))
            col.transform.parent.GetComponent<PlayerMovement3D>().IsOnStairs = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.parent.CompareTag("Player"))
            col.transform.parent.GetComponent<PlayerMovement3D>().IsOnStairs = false;
    }
}
