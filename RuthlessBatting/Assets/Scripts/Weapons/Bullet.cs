using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float speed;

    private Transform player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);

        Vector3 newPosition = target - transform.position;
        newPosition.y = 0;
        transform.rotation = Quaternion.LookRotation(newPosition, Vector3.up);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            AudioManager.Instance.RunAudio(Audios.bate_pared);
            DestroyBullet();
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
            AudioManager.Instance.RunAudio(Audios.matar_enemigo_bath);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
