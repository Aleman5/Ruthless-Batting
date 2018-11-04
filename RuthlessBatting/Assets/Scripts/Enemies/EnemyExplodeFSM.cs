using System.Collections;
using UnityEngine;

public class EnemyExplodeFSM : MonoBehaviour, IAttack
{
    [SerializeField] EnemyBomb fsmScript;
    [SerializeField] Health enemyHealth;
    [SerializeField] GameObject explosion;
    [SerializeField] float timeToExplode;

    public void Attack()
    {
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        Vector3 distance = fsmScript.GetDistance();

        fsmScript.enabled = false;

        yield return new WaitForSeconds(timeToExplode);

        Vector3 explosionPos = transform.position;
        explosionPos.y = 0.1f;
        Instantiate(explosion, explosionPos, transform.rotation);

        enemyHealth.Amount = 0;
    }
}
