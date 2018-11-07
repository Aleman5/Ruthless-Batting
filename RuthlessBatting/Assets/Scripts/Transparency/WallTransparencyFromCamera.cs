using UnityEngine;

public class WallTransparencyFromCamera : MonoBehaviour
{
    [Range(0, 0.99f)]
    [SerializeField] float levelOfTransparency = 0.3f;
    [SerializeField] LayerMask layerMask;

    SpriteRenderer SpriteRenderer;
    bool isTransparent;

    float distCameraPlayer;

    [SerializeField] Transform player;
    Transform tileTouched;

    void Start()
    {
        distCameraPlayer = (transform.position - player.transform.position).magnitude;

        isTransparent = false;
    }

    void LateUpdate ()
    {
        Debug.Log("Hola");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, distCameraPlayer, layerMask))
        {
            Debug.Log("Ah! me pegaron xd");
            if(!SpriteRenderer)
            {
                SpriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                MakeTransparent();
            }
        }
        else if(isTransparent)
        {
            TurnOffTransparent();
        }
                
	}

    void MakeTransparent()
    {
        if(SpriteRenderer.material.color.a != levelOfTransparency)
        {
            Debug.Log("Me estoy transparentando");
            Color color = SpriteRenderer.material.color;
            color.a = levelOfTransparency;
            SpriteRenderer.material.color = color;
            isTransparent = true;
        }
    }

    void TurnOffTransparent()
    {
        Debug.Log("Me estoy destransparentando");
        Color color = SpriteRenderer.material.color;
        color.a = 1.0f;
        SpriteRenderer.material.color = color;
        SpriteRenderer = null;
        isTransparent = false;
    }
}
