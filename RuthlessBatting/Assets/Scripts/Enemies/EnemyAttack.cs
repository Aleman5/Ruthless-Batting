using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyMovNav enemyMovement;
    [HideInInspector][SerializeField] UnityEvent onAttack;

    bool isRunning;

    BoxCollider box;

    void Awake()
    {
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

        //enemyMovement.IsStop();
        enemyMovement.enabled = false;

        yield return new WaitForSeconds(0.2f);

        ChangeBoxState();

        Utilities.GetDirection(transform, distance);

        onAttack.Invoke();

        yield return new WaitForSeconds(0.4f);

        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ChangeBoxState();

        yield return new WaitForSeconds(0.5f);

        //enemyMovement.IsStop();
        enemyMovement.enabled = true;
        enemyMovement.IsAttacking = false;

        isRunning = false;

        yield break;
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

    public UnityEvent OnAttack
    {
        get { return onAttack; }
    }
}
