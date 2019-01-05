using UnityEngine;

public class EnemyRangeAttackFSM : MonoBehaviour, IAttack
{
    [SerializeField] GameObject bullet;

    public void Attack()
    {
        AudioManager.Instance.RunAudio(Audios.bath_al_aire);
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
