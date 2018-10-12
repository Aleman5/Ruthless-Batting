using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] EnemyMovNav enemyMovement;
    bool isRunning;

    void Awake()
    {
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

        //enemyMovement.SetSpeed = 0.0f;
        enemyMovement.enabled = false;

        yield return new WaitForSeconds(0.2f);

        Utilities.SetBoxPreparations(transform, distance);

        yield return new WaitForSeconds(0.4f);

        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        yield return new WaitForSeconds(0.5f);

        enemyMovement.enabled = true;
        enemyMovement.IsAttacking = false;

        isRunning = false;

        yield break;
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
