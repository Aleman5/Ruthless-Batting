using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTransparency : MonoBehaviour
{
    [SerializeField] float maxDist;

    // Saving the last dist to make less operations per frame.
    float prevDist = 0.0f;

    void OnTriggerEnter(Collider col)
    {
        // Just remove it, it doens't matter if exists on the list or not.
        UpperFloorObjects.objects.Remove(col.GetComponent<SpriteRenderer>());
    }

    void OnTriggerStay(Collider col)
    {
        if (CompareTag("Player"))
        {
            float dist = (col.transform.position - transform.position).magnitude;

            if (dist != prevDist)
            {
                prevDist = dist;

                float percentage = dist / maxDist;

                if (percentage > 1.0f) percentage = 1.0f;

                UpperFloorObjects.ChangeTransparency(percentage);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        float dist = (col.transform.position - transform.position).magnitude;

        if(dist > maxDist / 2.0f) // This Collider is on the UpperFloor.
            UpperFloorObjects.objects.Add(col.GetComponent<SpriteRenderer>());
    }
}
