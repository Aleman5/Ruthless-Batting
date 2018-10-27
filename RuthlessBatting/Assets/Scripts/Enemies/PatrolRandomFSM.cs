using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolRandomFSM : MonoBehaviour, IPatrol
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

        do {
            destPoint = Random.Range(0, points.Length -1);
        } while (actualPoint == destPoint);

        agent.destination = points[destPoint].position;

        actualPoint = destPoint;
    }

    public void SetPoints(Transform[] pointsToFollow)
    {
        points = pointsToFollow;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        FindNextPoint();
    }
}
