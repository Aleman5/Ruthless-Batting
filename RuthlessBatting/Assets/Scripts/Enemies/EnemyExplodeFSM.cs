using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyExplodeFSM : MonoBehaviour, IAttack
{
    [SerializeField] EnemyBomb fsmScript;
    [SerializeField] Health enemyHealth;
    [SerializeField] GameObject explosion;
    [SerializeField] float timeToExplode;

    [HideInInspector][SerializeField] UnityEvent onExplode;

    void Awake()
    {
        enemyHealth.OnDeath().AddListener(Death);
    }

    public void Attack()
    {
        StartCoroutine("Explode");
    }

    void Death()
    {
        if (Explode().MoveNext())
            StopCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        //Vector3 distance = fsmScript.GetDistance();

        //fsmScript.enabled = false;

        yield return new WaitForSeconds(timeToExplode);

        Vector3 explosionPos = transform.position;
        explosionPos.y = 0.1f;
        Instantiate(explosion, explosionPos, transform.rotation);

        onExplode.Invoke();

        enemyHealth.Amount = 0;
    }

    public UnityEvent OnExplode()
    {
        return onExplode;
    }
}
