using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Vector3 mousePos;

    [SerializeField] float movSpeed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Moving with rigidbody
        Vector2 vecForce;
        vecForce.x = Input.GetAxis("Horizontal") * movSpeed;
        vecForce.y = Input.GetAxis("Vertical") * movSpeed;
        rb.AddForce(vecForce);

        // Rotation by the mouse position
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        // Moving with transform
        /*Vector3 movHor = Vector3.right * Input.GetAxis("Horizontal") * movSpeed;
        Vector3 movVer = Vector3.up * Input.GetAxis("Vertical") * movSpeed;
        transform.position += (movHor + movVer) * Time.deltaTime;*/
    }
}
