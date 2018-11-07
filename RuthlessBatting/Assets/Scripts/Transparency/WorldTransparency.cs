using UnityEngine;

public class WorldTransparency : MonoBehaviour
{
    [SerializeField] float maxDist;

    // Saving the last dist to make less operations per frame.
    float prevDist = 0.0f;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter");

        SpriteRenderer sprRend = col.transform.parent.GetComponentInChildren<SpriteRenderer>();

        // Just remove it, it doens't matter if exists on the list or not.
        if(UpperFloorObjects.objects.Remove(sprRend))
        {
            Color color = sprRend.material.color;
            color.a = 1.0f;
            sprRend.material.color = color;
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("OnTriggerStay");

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
        Debug.Log("OnTriggerExit");

        float dist = (col.transform.position - transform.position).magnitude;

        if(dist > maxDist / 2.0f) // This Collider is on the UpperFloor.
        {
            UpperFloorObjects.objects.Add(col.transform.parent.GetComponentInChildren<SpriteRenderer>());
        }
    }
}
