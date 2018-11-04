using UnityEngine;

public class EnemyRangeAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] GameObject bullet;

    public void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
