using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void LateUpdate ()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            
            MeshRenderer meshRenderer = hit.transform.GetComponent<MeshRenderer>();
            if(meshRenderer.material.color.a != 0.3f)
            {
                meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            }
        }
	}

    // Los objetos transparentados guardarlos en una lista o algo así y cuando el jugador deje de estar
    // por debajo que lo vuelva a la normalidad.
}
