using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float speed;

    private Transform player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
            DestroyBullet();
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();
            health.Amount -= 1;
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
