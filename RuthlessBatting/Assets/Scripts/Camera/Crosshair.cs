using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Canvas myCanvas;

	void Start ()
    {
        Cursor.visible = false;
	}

	void Update ()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

}
