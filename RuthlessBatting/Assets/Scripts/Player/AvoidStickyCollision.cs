using UnityEngine;

public class AvoidStickyCollision : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = transform.GetChild(0).GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Floor"))
            transform.position += new Vector3(0.0f, 0.01f, 0.0f);
    }

}
