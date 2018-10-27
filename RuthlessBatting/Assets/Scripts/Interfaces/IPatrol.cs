using UnityEngine;

public interface IPatrol
{
    void FindNextPoint();
    void SetPoints(Transform[] pointsToFollow);
}
