using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour {

    [SerializeField] EnemyMovNav enemyMovement;
    [SerializeField] Health enemyHealth;
    [SerializeField] GameObject explosion;
    [SerializeField] float timeToExplode;
    bool isRunning;

    void Awake()
    {
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

        enemyMovement.IsStop();

        Vector3 explosionPos = transform.position;
        explosionPos.y = 0.1f;
        Instantiate(explosion, explosionPos, transform.rotation);

        enemyHealth.Amount = 0;
    }
}
