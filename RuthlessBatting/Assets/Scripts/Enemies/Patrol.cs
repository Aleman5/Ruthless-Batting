﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Patrol : MonoBehaviour
{
    NavMeshAgent agent;
    Transform[] points;
    int destPoint = 0;
    int actualPoint;

    void Start()
    {
        actualPoint = -1;
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;
    }

    void FindNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        do{
            destPoint = (destPoint + 1) % points.Length;
        } while (actualPoint == destPoint);

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        actualPoint = destPoint;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        //destPoint = (destPoint + 1) % points.Length;
        
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            FindNextPoint();
    }

    public void SetPoints(Transform[] pointsToFollow)
    {
        points = pointsToFollow;
    }
}