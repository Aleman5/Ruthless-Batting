using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDirection : MonoBehaviour {

    [SerializeField] Transform obj;

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, obj.transform.position.y));
        Vector3 forward = mouseWorld - obj.transform.position;
        obj.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
