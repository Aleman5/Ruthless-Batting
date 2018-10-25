using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    [SerializeField] float timeToDestroy;

    public Autodestroy(float timeToDestroy)
    {
        this.timeToDestroy = timeToDestroy;
    }

    void Awake()
    {
        Invoke("DestroyMySelf", timeToDestroy);
    }

    void DestroyMySelf()
    {
        Destroy(gameObject);
    }
}
