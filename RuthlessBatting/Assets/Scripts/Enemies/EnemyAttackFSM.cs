using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackFSM : MonoBehaviour
{
    [SerializeField] Enemy fsmScript;
    [HideInInspector] [SerializeField] UnityEvent onAttack;

    BoxCollider box;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        box.enabled = false;
    }

    void Update()
    {
        if (fsmScript.GetActualState() == 2)
            StartCoroutine(Attack(fsmScript.GetDistance()));
    }

    IEnumerator Attack(Vector3 distance)
    {
        fsmScript.enabled = false;

        yield return new WaitForSeconds(0.2f);

        ChangeBoxState();

        Utilities.GetDirection(transform, distance);

        onAttack.Invoke();

        yield return new WaitForSeconds(0.4f);

        transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ChangeBoxState();

        yield return new WaitForSeconds(0.5f);

        fsmScript.enabled = true;

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
