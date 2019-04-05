using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCameraController : MonoBehaviour
{
    public float rotationSpeed = 3;
    private Transform parentTarget;
    private float mouseX;
    private float mouseY;
    public Transform neck;
    public Transform head;
    public Transform spine;
    private Animator anim;
    private float tempMouseX;
    [HideInInspector]public bool canMove;
    public bool isFalling;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.parent.GetComponent<Animator>();
        parentTarget = transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        if (canMove)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseX = Mathf.Clamp(mouseX, -90, 90);
            mouseY = Mathf.Clamp(mouseY, -35, 60);
            parentTarget.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else if (isFalling)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
            //mouseX = Mathf.Clamp(mouseX, -90, 90);
            mouseY = Mathf.Clamp(mouseY, -35, 60);
            parentTarget.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
    }

    private void LateUpdate()
    {
        if (canMove)
        {
            //Move head with camera
            if (Mathf.Abs(mouseX) < 40)
            {
                tempMouseX = 0;
            }
            else if (mouseX < 0)
            {
                tempMouseX = mouseX + 40;
            }
            else
            {
                tempMouseX = mouseX - 40;
            }
            neck.rotation = Quaternion.Euler(neck.transform.rotation.x, tempMouseX, neck.transform.rotation.z);
            head.rotation = Quaternion.Euler(mouseY, tempMouseX, head.transform.rotation.z);
            //More thought is required to make the turn look more natural. Return here i suppose
            //NVM
            //Still need to do hands/arms i guess
            spine.rotation = Quaternion.Euler(spine.transform.rotation.x, Mathf.Clamp(mouseX, -40, 40), spine.transform.rotation.z);


            //anim.SetBoneLocalRotation(HumanBodyBones.Neck, Quaternion.Euler(neck.transform.rotation.x, mouseX, neck.transform.rotation.z));
            //anim.SetBoneLocalRotation(HumanBodyBones.Head, Quaternion.Euler(mouseY, head.transform.rotation.y, head.transform.rotation.z));


            //neck.rotation = Quaternion.Euler(mouseY, neck.transform.rotation.y, neck.transform.rotation.z);
            //head.rotation = Quaternion.Euler(head.transform.rotation.x, mouseX, head.transform.rotation.z);
            //anim.SetBoneLocalRotation(HumanBodyBones.Neck, Quaternion.Euler(mouseY, neck.transform.rotation.y, neck.transform.rotation.z));
            //anim.SetBoneLocalRotation(HumanBodyBones.Head, Quaternion.Euler(head.transform.rotation.x, mouseX, head.transform.rotation.z));
        }
    }

    public void StartFall()
    {
        isFalling = true;
        anim.SetTrigger("StartFall");
    }
}
