using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 mousePos;

    void Update()
    {
        /*mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec = player.transform.position - mousePos;
        vec.z = 0;
        if(vec.magnitude < 4 && vec.magnitude > 1)
        {
            vec /= 2;
            transform.position = mousePos + vec;
        }*/
    }
}
