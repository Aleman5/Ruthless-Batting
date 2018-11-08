using UnityEngine.AI;
using UnityEngine;

public class EnemyMovNav : MonoBehaviour {

    [Range(2, 100)]
    [SerializeField] float distToChase;
    [Range(1, 99)]
    [SerializeField] float distToStop;
    [Range(1, 6)]
    [SerializeField] float speed;
    [SerializeField] LayerMask possibleObstacules;

    Transform player;
    NavMeshAgent nav;
    //Animator anim;

    bool isChasing;

    void Awake()
    {
        player = GameObject.Find("Player_Limit").transform;
        //anim = GetComponent<Animator>();

        nav = GetComponentInParent<NavMeshAgent>();
        nav.angularSpeed = 0;
        nav.speed = speed;

        isChasing = false;
    }


    void Update()
    {
        Invoke("SetDestiny", 1.5f);
    }

    void SetDestiny()
    {
        Vector3 diff = player.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (nav.enabled && dist < distToChase)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
            {
                if (dist > distToStop)
                {
                    if(!isChasing) isChasing = true;
                    nav.SetDestination(player.position);
                    nav.speed = speed * 2;
                }
                else
                {
                    if(isChasing) isChasing = false;
                    IsAttacking = true;
                }
            }
            else
            {
                if (isChasing) isChasing = false;
                nav.speed = speed;
            }
        }
    }

    public int GetDirection()
    {
        return Utilities.GetDirection(transform, nav.destination - nav.transform.position);
    }

    public Vector3 GetDistance()
    {
        return player.position - transform.position;
    }

    public bool IsChasing { get { return isChasing; } }

    public bool IsAttacking { get; set; }
    //public float SetSpeed { set { nav.speed = value; } }
    public void IsStop()
    {
        nav.isStopped = !nav.isStopped;
        nav.velocity = Vector3.zero;
    }
}
