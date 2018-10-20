using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimDirection : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float length;

    void Start()
    {
        player = transform.parent.transform;
    }

    void Update()
    {
        Vector3 playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.position;
        playerToMouseDir.z = 0;
        playerToMouseDir.y = 0;

        Debug.Log(playerToMouseDir);

        transform.position = player.position + (length * playerToMouseDir.normalized);
    }
}