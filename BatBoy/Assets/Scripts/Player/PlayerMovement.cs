using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //Vector3 mousePos;

    [SerializeField] float movSpeed;

    Rigidbody2D rb;
    Vector2 movForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movForce = Vector2.zero;
        movForce.x = Input.GetAxis("Horizontal") * movSpeed;
        movForce.y = Input.GetAxis("Vertical") * movSpeed;
        

        // Rotation by the mouse position
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        // Moving with transform
        /*Vector3 movHor = Vector3.right * Input.GetAxis("Horizontal") * movSpeed;
        Vector3 movVer = Vector3.up * Input.GetAxis("Vertical") * movSpeed;
        transform.position += (movHor + movVer) * Time.deltaTime;*/
    }

    void FixedUpdate()
    {
        rb.AddForce(movForce);
    }
}
