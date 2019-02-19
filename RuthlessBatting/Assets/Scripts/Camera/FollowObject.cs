using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject player;

    Vector3 offset; // Distance between Camera and Player

    void Start()
    {
        player.GetComponent<Health>().OnDeath().AddListener(DisableScript);
        offset = transform.position - player.transform.position;
    }

    void LateUpdate() // Im using LateUpdate because its needed the actual position of the Player
    {
        transform.position = player.transform.position + offset;
    }
    
    void DisableScript()
    {
        enabled = false;
    }
}
