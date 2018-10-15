using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour {

    [SerializeField] EnemyMovNav enemyMovement;
    [SerializeField] Health enemyHealth;
    [SerializeField] float timeToExplode;
    bool isRunning;

    SphereCollider sphere;

    void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        sphere.enabled = false;

        isRunning = false;
    }

    void Update()
    {
        if (enemyMovement.IsAttacking && !isRunning)
            StartCoroutine(Explode(enemyMovement.GetDistance()));
    }

    IEnumerator Explode(Vector3 distance)
    {
        isRunning = true;

        enemyMovement.IsStop();
        enemyMovement.enabled = false;

        yield return new WaitForSeconds(timeToExplode);

        ChangeBoxState();

        yield return new WaitForSeconds(0.1f);
        ChangeBoxState();

        enemyMovement.IsStop();

        enemyHealth.Amount = 0;
    }

    private void ChangeBoxState()
    {
        sphere.enabled = !sphere.enabled;
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
