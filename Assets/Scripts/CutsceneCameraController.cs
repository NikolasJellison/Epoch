using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneCameraController : MonoBehaviour
{
    public enum CutsceneType {One, Last }
    public CutsceneType cutsceneType;
    public float rotationSpeed = 3;
    private Transform parentTarget;
    private float mouseX;
    private float mouseY;
    public Transform neck;
    public Transform head;
    public Transform spine;
    private Animator anim;
    private float tempMouseX;
    [HideInInspector]public bool screenOn;
    [HideInInspector]public bool canMove;
    public bool enteredDesktop;
    public bool isFalling;
    public TextMeshProUGUI notificationText;
    private CutScene1 cutsceneOne;
    private CutSceneLast cutSceneLast;
   // public CutsceneType orientation;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.parent.GetComponent<Animator>();
        parentTarget = transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(cutsceneType == CutsceneType.One)
        {
            cutsceneOne = GameObject.Find("CutScene Manager").GetComponent<CutScene1>();
        }
        else if((cutsceneType == CutsceneType.Last))
        {
            cutSceneLast = GameObject.Find("CutScene Manager").GetComponent<CutSceneLast>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //This bool - 'canMove' gets triggered from an event on the "crying" animation
        if (canMove)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseX = Mathf.Clamp(mouseX, -90, 90);
            mouseY = Mathf.Clamp(mouseY, -35, 60);
            parentTarget.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            if(Mathf.Abs(mouseX) < 15 && Mathf.Abs(mouseY) < 15 && screenOn && !enteredDesktop)
            {
                //notificationText.text = "Press <sprite=\"EKey02\" index=\"0\"> to inspect";
                notificationText.text = "Left click to inspect";
                //I feel like 2 nested If statements is bad...
                //if (Input.GetKeyDown(KeyCode.E))
                if (Input.GetMouseButtonDown(0))
                {
                    //canMove gets disabled in Cutscene1.cs after the dissolve takes place
                    enteredDesktop = true;
                    if (cutsceneType == CutsceneType.One)
                    {
                        cutsceneOne.EnterDesktop();
                    }
                    else if ((cutsceneType == CutsceneType.Last))
                    {
                        cutSceneLast.EnterDesktop();
                    }
                    //This is a mess because i didn't want to put another public variable for some reason
                    notificationText.gameObject.transform.parent.gameObject.SetActive(false);
                }
            }
            else
            {
                notificationText.text = "";
            }
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

    public void StartLook()
    {
        isFalling = false;
        anim.SetTrigger("StartLook");
        //Need to reenable the canvas so we can fade
        notificationText.text = "";
        notificationText.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void StartWalk()
    {
        anim.SetTrigger("StartWalk");
    }
}
