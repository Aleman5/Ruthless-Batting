using UnityEngine;

public class ZoomWhenKilling : MonoBehaviour
{
    [SerializeField] float distanceToZoom;
    [SerializeField] float velocityTurningBack;
    [SerializeField] float minCameraSize;
    [SerializeField] float maxCameraSize;
    bool isGoingBack;

    void Update()
    {
        if (isGoingBack && Camera.main.orthographicSize < maxCameraSize)
        {
            Camera.main.orthographicSize += velocityTurningBack;
        }
    }

    public void ReduceSize()
    {
        if(Camera.main.orthographicSize > minCameraSize)
            Camera.main.orthographicSize -= distanceToZoom;
        isGoingBack = false;
    }

    public void SetNewMaxSize(float newSize)
    {
        maxCameraSize = newSize;
        IsGoingBack = true;
    }

    public bool IsGoingBack
    {
        get { return isGoingBack;  }
        set { isGoingBack = value; }
    }
}
