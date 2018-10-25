using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolRandom : MonoBehaviour
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

    void FindNextPoint()
    {
        if (points.Length == 0)
            return;

        do {
            destPoint = Random.Range(0, points.Length -1);
        } while (actualPoint == destPoint);

        agent.destination = points[destPoint].position;

        actualPoint = destPoint;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StartCoroutine(Wait());
                //FindNextPoint();
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
