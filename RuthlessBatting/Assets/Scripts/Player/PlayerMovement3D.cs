using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbodyToUse;
    [SerializeField] float speed;
    [SerializeField] float speedOnStairs;
    [SerializeField] float dashForce;

    bool isOnStairs = false;
    float angleOfTheStairs = 36.0f; // Angle of the Stairs.
    float originalMovSpeed;
    Vector3 movForce;
    Vector3 stairsDir;

    void Start()
    {
        originalMovSpeed = speed;

        stairsDir.x = 0.0f;
        //stairsDir.y = Mathf.Sin(angleOfTheStairs);
        //stairsDir.z = Mathf.Cos(angleOfTheStairs);

        stairsDir.y = 0.587785f;
        stairsDir.z = 0.809017f;
    }

    void Update()
    {
        movForce = Vector3.zero;

        movForce.x = InputManager.Instance.GetHorizontalAxis();
        movForce.z = InputManager.Instance.GetVerticalAxis();

        if (!isOnStairs)
        {
            movForce.x *= speed;
            movForce.z *= speed;
            movForce.y = 0;
        }
        else
        {
            movForce.z *= stairsDir.z;

            movForce.y = movForce.z * stairsDir.y;

            movForce = movForce.normalized * speedOnStairs;
        }

        if (InputManager.Instance.GetDashButton())
        {
            MakeForceMovement();
        }

        // Para Testing nomás
        if (Input.GetKey(KeyCode.H))
        {
            GetComponent<Health>().Amount++;
        }
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
        speed = originalMovSpeed + originalMovSpeed * (0.05f * level); // Level 1 -> +5%, Level 2 -> +10%, Level 3 -> +15%
    }

    public bool IsOnStairs { set { isOnStairs = value; }  }
}
