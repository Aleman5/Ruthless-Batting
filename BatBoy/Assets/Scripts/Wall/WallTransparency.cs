using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    [SerializeField] Transform player;
    //[SerializeField] MeshRenderer[] tilesTouched; // Temporary Serializabed

    Transform tileTouched;
    float distCameraPlayer;

    void Start()
    {
        distCameraPlayer = (transform.position - player.transform.position).magnitude;
    }

    void LateUpdate ()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, distCameraPlayer))
        {
            if (hit.transform != tileTouched)
            {
                MeshRenderer meshRenderer = hit.transform.GetComponent<MeshRenderer>();

                Color color = meshRenderer.material.color;
                color.a = 0.3f;
                meshRenderer.material.color = color;
                tileTouched = hit.transform;
            }
        }
        else if(tileTouched)
        {
            Color color = tileTouched.GetComponent<MeshRenderer>().material.color;
            color.a = 1f;
            tileTouched.GetComponent<MeshRenderer>().material.color = color;
            tileTouched = null;
        }
	}
}
