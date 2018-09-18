using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfTargerBehindWall : MonoBehaviour
{
    [SerializeField] float rangeToHunt;
    [SerializeField] float rangeToStop;
    [SerializeField] LayerMask possibleObstacules;

    GameObject target;

    public bool CheckMovement()
    {
        bool isMoving = false;

        Vector3 diff = target.transform.position - transform.position;
        Vector3 dir = diff.normalized;
        float dist = diff.magnitude;

        if (dist < rangeToHunt)
        {
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, dir, out hit, dist, possibleObstacules))
            {
                if (dist > rangeToStop)
                {
                    isMoving = true;
                }
                else
                {
                    IsAttacking = true;
                }
            }
        }

        return isMoving;
    }

    public bool IsAttacking { get; set; }
}
