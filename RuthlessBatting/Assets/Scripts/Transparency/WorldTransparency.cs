using UnityEngine;

public class WorldTransparency : MonoBehaviour
{
    [SerializeField] float maxDist;

    // Saving the last dist to make less operations per frame.
    float prevDist = 0.0f;

    void OnTriggerEnter(Collider col)
    {
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
        if (col.transform.parent.CompareTag("Player"))
        {
            float dist = (col.transform.position - transform.position).magnitude;

            if (dist != prevDist)
            {
                prevDist = dist;

                float percentage = dist / maxDist;

                if (percentage > 1.0f) percentage = 1.0f;
                else if (percentage < 0.1f) percentage = 0.1f;

                UpperFloorObjects.ChangeTransparency(percentage);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        float dist = (col.transform.position - transform.position).magnitude;

        if(dist > maxDist / 2.0f) // This Collider is on the UpperFloor.
        {
            UpperFloorObjects.objects.Add(col.transform.parent.GetComponentInChildren<SpriteRenderer>());
        }
        else if (col.transform.parent.CompareTag("Player"))
            UpperFloorObjects.ChangeTransparency(0.1f);
    }
}
