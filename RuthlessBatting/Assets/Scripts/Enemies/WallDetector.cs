using UnityEngine;

public class WallDetector : MonoBehaviour
{
    TransparentableWall wall;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Wall"))
            wall = col.GetComponent<TransparentableWall>();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Wall"))
        {
            wall = null;
        }
    }

    void OnDestroy()
    {
        if (wall)
        {
            wall.DecreaseCounter();
        }
    }
}
