using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float sensitivity = 1f;
    //private CinemachineComposer composer;
    public float rotation_speed;
    //public float slide_speed;
    public GameObject player;
    public Transform target;
    private PlayerController player_controller;
    private bool crouching;
    private bool standing;
    private Vector3 start_postion;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        crouching = false;
        Cursor.lockState = CursorLockMode.Locked;
        player_controller = player.GetComponent<PlayerController>();
        start_postion = transform.position;

    }



    // Update is called once per frame
    void LateUpdate()
    {
        camControl();
    }

    void camControl()
    {

        if (!player_controller.IsCrouched())
        {


            mouseX += Input.GetAxis("Mouse X") * rotation_speed;
            mouseY -= Input.GetAxis("Mouse Y") * rotation_speed;



            mouseX += Input.GetAxis("Mouse X") * rotation_speed;
            mouseY -= Input.GetAxis("Mouse Y") * rotation_speed; 
            mouseY = Mathf.Clamp(mouseY, -35, 60);

            transform.LookAt(target);

            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            if (!player_controller.IsManip())
            {
                player.transform.rotation = Quaternion.Euler(0, mouseX, 0);
            }
            
        }


    }
}
