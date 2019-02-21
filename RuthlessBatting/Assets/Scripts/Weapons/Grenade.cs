using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed;
    [SerializeField] float maxDistance;

    Rigidbody rb;
    Animator anim;
    Vector3 movement;

    float timeToDestroy = 0.5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  
    }

    void Start()
    {
        anim.SetBool("move", true);
    }

    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy < 0) Explote();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision col)
    {
        Explote();
    }

    void Explote()
    {
        Vector3 explosionPos = transform.position;
        explosionPos.y = 0.1f;
        Instantiate(explosion, explosionPos, transform.rotation);

        AudioManager.Instance.RunAudio(Audios.granada);

        Destroy(gameObject);
    }

    public void SetData(Vector3 direction, Vector3 destination)
    {
        movement = direction * speed;

        Vector3 newDestination = destination - transform.position;
        newDestination.y = 0;

        if (newDestination.magnitude > maxDistance)
        {
            newDestination.Normalize();
            newDestination *= maxDistance;
        }

       transform.rotation = Quaternion.LookRotation(newDestination, Vector3.down);
    }
}
