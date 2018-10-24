using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWhenKilling : MonoBehaviour
{
    [SerializeField] float distanceToZoom;
    [SerializeField] float velocityTurningBack;
    [SerializeField] float minCameraSize;

    float maxSizeOfView;
    bool isGoingBack;

    void Awake()
    {
        maxSizeOfView = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (isGoingBack && Camera.main.orthographicSize < maxSizeOfView)
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

    public bool IsGoingBack
    {
        get { return isGoingBack;  }
        set { isGoingBack = value; }
    }
}
