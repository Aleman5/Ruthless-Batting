using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 offset; // Distance between Camera and Player

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() // Im using LateUpdate because its needed the actual position of the Player
    {
        transform.position = player.transform.position + offset;

        /*Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec = player.transform.position - mousePos;
        vec.z = 0;
        if(vec.magnitude < 4 && vec.magnitude > 1)
        {
            vec /= 2;
            transform.position = mousePos + vec;
        }*/
    }
}
