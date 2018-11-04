using UnityEngine;

public class PlayerRotationThroughMouse : MonoBehaviour
{
    Vector3 mousePos;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
