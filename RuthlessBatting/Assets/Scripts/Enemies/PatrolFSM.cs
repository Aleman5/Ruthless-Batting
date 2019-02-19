using UnityEngine;
using UnityEngine.AI;

public class PatrolFSM : MonoBehaviour, IPatrol
{
    NavMeshAgent agent;
    Transform[] points;
    int destPoint = 0;
    int actualPoint;

    void Start()
    {
        actualPoint = -1;
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;
    }

    public void FindNextPoint()
    {
        if (points.Length == 0)
            return;
        
        do{
            destPoint = (destPoint + 1) % points.Length;
        } while (actualPoint == destPoint);

        agent.destination = points[destPoint].position;

        actualPoint = destPoint;
    }

    /*void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            FindNextPoint();
    }*/

    public void SetPoints(Transform[] pointsToFollow)
    {
        points = pointsToFollow;
    }
}
