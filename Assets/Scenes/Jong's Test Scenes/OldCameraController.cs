using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldCameraController : MonoBehaviour
{
    //public float sensitivity = 1f;
    //private CinemachineComposer composer;
    public float rotation_speed;
    public float slide_speed;
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
        //composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    //private void Update()
    //{
    //    float vertical = Input.GetAxis("Mouse Y") * sensitivity;
    //    composer.m_TrackedObjectOffset.y += vertical;
    //    composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, 0, 4);
    //}

    // Update is called once per frame
    void LateUpdate()
    {
        //if (player_controller.IsCrouched())
        //{
        //    crouching = true;
        //}else standing |= (!player_controller.IsCrouched() && crouching == true);
        camControl();
    }

    void camControl()
    {
        //if(standing && transform.position != start_postion)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, start_postion, slide_speed * Time.deltaTime);

        //}else if(standing && transform.position == start_postion)
        //{
        //    standing = false;
        //}
        //if (crouching && transform.position != crouched_pos.position)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, crouched_pos.position, slide_speed * Time.deltaTime);
        //}else if(crouching && transform.position == crouched_pos.position)
        //{
        //    crouching = false;
        //}
        if (!player_controller.IsCrouched())
        {
            if (!player_controller.lock_movement)
            {

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
}
