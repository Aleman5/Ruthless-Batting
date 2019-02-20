using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float speed;
    Animator anim;

    Rigidbody rb;
    Vector3 movement;
    Vector3 dest;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();  
    }

    private void Start()
    {
        anim.SetBool("move", true);
    }

    void Update()
    {
        Vector3 diff = dest - transform.position;
        diff.y = 0;
        float dist = diff.magnitude;

        if(dist < 0.3f) Explote();
    }

    void FixedUpdate()
    {
        rb.AddForce(movement);
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.transform.CompareTag("Wall"))
            Explote();
    }

    void Explote()
    {
        Vector3 explosionPos = transform.position;
        explosionPos.y = 0.1f;
        Instantiate(explosion, explosionPos, transform.rotation);

        Destroy(gameObject);
    }

    public void SetData(Vector3 direction, Vector3 destination)
    {
        movement = direction * speed;
        dest = destination;
    }
}
