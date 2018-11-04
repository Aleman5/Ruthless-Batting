using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyToUse;
    [SerializeField] float movSpeed;
    [SerializeField] float dashForce;

    float originalMovSpeed;
    Vector3 movForce;

    void Start()
    {
        originalMovSpeed = movSpeed;
    }

    void Update()
    {
        movForce = Vector3.zero;
        movForce.x = InputManager.Instance.GetHorizontalAxis() * movSpeed;
        movForce.z = InputManager.Instance.GetVerticalAxis() * movSpeed;
        movForce.y = 0;

        /*if (InputManager.Instance.GetDashButton())
        {
            MakeForceMovement();
        }*/
            

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

    public void MakeForceMovement()
    {
        rigidbodyToUse.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        Vector3 newForce = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        newForce.y = 0;
        newForce = newForce.normalized * dashForce;

        // Dash depending on the player LookAt
        /*if (movForce.z > 0)
            newForce = Vector3.forward * dashForce;
        else if (movForce.z < 0)
            newForce = -Vector3.forward * dashForce;
        else if (movForce.x > 0)
            newForce = Vector3.right * dashForce;
        else
            newForce = -Vector3.right * dashForce;*/

        rigidbodyToUse.AddForce(newForce);
    }

    public void SetStats(int level)
    {
        movSpeed = originalMovSpeed + originalMovSpeed * (0.05f * level); // Level 1 -> +5%, Level 2 -> +10%, Level 3 -> +15%
    }
}
