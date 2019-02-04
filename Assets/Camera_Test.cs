using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Test : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    float distance;
    Vector3 playerPrevPos, playerMoveDir;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;

        distance = offset.magnitude;
        playerPrevPos = player.transform.position;
    }

    void LateUpdate()
    {

        playerMoveDir = player.transform.position - playerPrevPos;
        playerMoveDir.Normalize();
        transform.position = player.transform.position - playerMoveDir * distance;

        transform.LookAt(player.transform.position);

        playerPrevPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
