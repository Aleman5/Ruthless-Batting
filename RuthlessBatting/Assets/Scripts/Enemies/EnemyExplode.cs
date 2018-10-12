using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour {

    [SerializeField] EnemyMovNav enemyMovement;
    [SerializeField] Health enemyHealth;
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

        yield return new WaitForSeconds(1f);

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
        Debug.Log("ONTRIGGER");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ONTRIGGERPLAYER");
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
        }
    }
}
