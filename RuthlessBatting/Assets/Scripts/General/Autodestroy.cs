using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    [SerializeField] float timeToDestroy;

    void Awake()
    {
        Invoke("DestroyMySelf", timeToDestroy);
    }

    void DestroyMySelf()
    {
        Destroy(gameObject);
    }
}
