using UnityEngine;

public class AvoidStickyCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Floor"))
            transform.position += new Vector3(0.0f, 0.01f, 0.0f);
    }

}
