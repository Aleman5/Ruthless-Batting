using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void SetBoxPreparations(Transform objectToPrepare, Vector3 distance)
    {
        distance.y = 0;
        float angle = Vector3.SignedAngle(distance, objectToPrepare.forward, Vector3.up);

        if (angle > 45 && angle < 135)
            objectToPrepare.eulerAngles = new Vector3(0f, -90f, 0f);
        else if (angle >= 135 || angle <= -135)
            objectToPrepare.eulerAngles = new Vector3(0f, 180f, 0f);
        else if (angle < -45 && angle > -135)
            objectToPrepare.eulerAngles = new Vector3(0f, 90f, 0f);
        else
            objectToPrepare.eulerAngles = new Vector3(0f, 0f, 0);
    }
}
