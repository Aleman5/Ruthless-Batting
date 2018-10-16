using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomWhenKilling : MonoBehaviour
{
    public static ZoomWhenKilling instance;

    [SerializeField] float distanceToGetCloser;
    [SerializeField] float velocityGoingBack;
    [SerializeField] float timeToStartGoingBack;

    float maxSizeOfView;
    float actualTime;

    void Awake()
    {
        maxSizeOfView = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (Camera.main.orthographicSize < maxSizeOfView)
        {
            if(actualTime < 0)
            {
                Camera.main.orthographicSize += velocityGoingBack;
                return;
            }
            actualTime -= Time.deltaTime;
        }
    }

    public void ReduceSize()
    {
        Camera.main.orthographicSize -= distanceToGetCloser;
        actualTime = timeToStartGoingBack;
    }

    static public ZoomWhenKilling Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<ZoomWhenKilling>();
                if (!instance)
                {
                    GameObject go = new GameObject("ZoomWhenKilling");
                    instance = go.AddComponent<ZoomWhenKilling>();
                }
            }
            return instance;
        }
    }
}
