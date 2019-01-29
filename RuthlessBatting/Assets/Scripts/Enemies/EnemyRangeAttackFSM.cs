using UnityEngine;

public class EnemyRangeAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] GameObject bullet;

    public void Attack()
    {
        AudioManager.Instance.RunAudio(Audios.disparo_laser);
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
