using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotation_speed;
    public float slide_speed;
    public GameObject player;
    public Transform crouched_pos;
    public Transform Third_Person_target;
    public Transform Crouch_Target;
    public Transform start_position;
    private Transform target;
    private PlayerController player_controller;
    private bool crouching;
    private bool standing;
    //private Vector3 start_postion;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        crouching = false;
        standing = false;
        Cursor.lockState = CursorLockMode.Locked;
        player_controller = player.GetComponent<PlayerController>();
        //start_postion = transform.position;
        target = Third_Person_target;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player_controller.IsCrouched())
        {
            crouching = true;
            transform.parent = transform.parent.parent.GetChild(14);
            target = Crouch_Target;
        }else if (!player_controller.IsCrouched() && crouching == true)
        {
            standing = true;
            transform.parent = transform.parent.parent.GetChild(13);
            target = Third_Person_target;
        }


        CamControl();
    }

    void CamControl()
    {
        if(standing && transform.position != start_position.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, start_position.position, slide_speed * Time.deltaTime);

        }else if (crouching && transform.position != crouched_pos.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, crouched_pos.position, slide_speed * Time.deltaTime);
        }
        mouseX += Input.GetAxis("Mouse X") * rotation_speed;
        mouseY -= Input.GetAxis("Mouse Y") * rotation_speed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        player.transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
