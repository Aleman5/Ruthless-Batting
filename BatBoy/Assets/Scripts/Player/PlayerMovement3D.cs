using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyToUse;
    [SerializeField] float movSpeed;

    float originalMovSpeed;
    Vector3 movForce;

    void Start()
    {
        originalMovSpeed = movSpeed;
    }

    void Update()
    {
        movForce = Vector3.zero;
        movForce.x = Input.GetAxis("Horizontal") * movSpeed;
        movForce.z = Input.GetAxis("Vertical") * movSpeed;
        movForce.y = 0;
        

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
        rigidbodyToUse.AddForce(movForce);
    }

    public void SetStats(int level)
    {
        movSpeed = originalMovSpeed + originalMovSpeed * (0.1f * level); // Level 1 -> +10%, Level 2 -> +20%, Level 3 -> +30%
    }
}
