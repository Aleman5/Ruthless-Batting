using UnityEngine;

public class Utilities : MonoBehaviour
{
    // 0 -> Up; 1 -> Right; 2 -> Down; 3 -> Left
    public static int GetDirection(Transform objectToPrepare, Vector3 distance)
    {
        distance.y = 0;
        float angle = Vector3.SignedAngle(distance, Vector3.forward, Vector3.up);

        if (angle > 45 && angle < 135)
            return 3;
        else if (angle >= 135 || angle <= -135)
            return 2;
        else if (angle < -45 && angle > -135)
            return 1;
        else
            return 0;
    }

    public static void SetBoxPreparations(Transform objToPrepare, int direction)
    {
        switch (direction)
        {
            case 0:
                objToPrepare.eulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case 1:
                objToPrepare.eulerAngles = new Vector3(0f, 90f, 0f);
                break;
            case 2:
                objToPrepare.eulerAngles = new Vector3(0f, 180f, 0f);
                break;
            case 3:
                objToPrepare.eulerAngles = new Vector3(0f, -90f, 0f);
                break;
        }
    }
}
