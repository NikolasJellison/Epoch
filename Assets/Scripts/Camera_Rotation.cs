using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour
{
    //public float sensitivity = 1f;
    //private CinemachineComposer composer;
    //public float rotation_speed;
    //public float slide_speed;
    public GameObject player;
    //public Transform target;
    private PlayerController player_controller;
    //private bool crouching;
    //private bool standing;
    //private Vector3 start_postion;
    //float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player_controller = player.GetComponent<PlayerController>();

    }

   

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player_controller.IsCrouched())
        {
            player.transform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        }
    }


}
