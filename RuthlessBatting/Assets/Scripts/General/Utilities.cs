using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    // 0 -> Up; 1 -> Right; 2 -> Down; 3 -> Left
    public static int SetBoxPreparations(Transform objectToPrepare, Vector3 distance)
    {
        distance.y = 0;
        float angle = Vector3.SignedAngle(distance, objectToPrepare.forward, Vector3.up);

        if (angle > 45 && angle < 135)
        {
            objectToPrepare.eulerAngles = new Vector3(0f, -90f, 0f);
            return 3;
        }
        else if (angle >= 135 || angle <= -135)
        {
            objectToPrepare.eulerAngles = new Vector3(0f, 180f, 0f);
            return 2;
        }
        else if (angle < -45 && angle > -135)
        {
            objectToPrepare.eulerAngles = new Vector3(0f, 90f, 0f);
            return 1;
        }
        else
        {
            objectToPrepare.eulerAngles = new Vector3(0f, 0f, 0);
            return 0;
        }
    }
}
