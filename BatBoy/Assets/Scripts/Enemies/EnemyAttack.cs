using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyMovement3D enemyMovement;
    bool isRunning;

    BoxCollider box;

    void Awake()
    {
        enemyMovement = GetComponentInParent<EnemyMovement3D>();

        box = GetComponent<BoxCollider>();
        box.enabled = false;

        isRunning = false;
    }

    void Update()
    {
        if (enemyMovement.IsAttacking && !isRunning)
            StartCoroutine(Attack(enemyMovement.GetDistance()));
    }

    IEnumerator Attack(Vector3 distance)
    {
        isRunning = true;

        enemyMovement.enabled = false;

        ChangeBoxState();

        SetDirection(distance);

        yield return new WaitForSeconds(0.5f);

        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ChangeBoxState();

        yield return new WaitForSeconds(0.5f);

        enemyMovement.enabled = true;
        enemyMovement.IsAttacking = false;

        isRunning = false;

        yield break;
    }

    void SetDirection(Vector3 distance)
    {
        distance.y = 0;
        float angle = Vector3.SignedAngle(distance, transform.forward, Vector3.up);

        if (angle > 45 && angle < 135)
            transform.eulerAngles = new Vector3(0f, -90f, 0f);
        else if (angle >= 135 || angle <= -135)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        else if (angle < -45 && angle > -135)
            transform.eulerAngles = new Vector3(0f, 90f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0);
    }

    private void ChangeBoxState()
    {
        box.enabled = !box.enabled;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
        }
    }
}
