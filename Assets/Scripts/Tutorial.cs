using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    //public Text tutorialText
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI extraText;
    //Tracking steps in tutorial?
    public GameObject vantageManager;
    private bool walked;

    public float freeTimeSpeed;
    public float delay;
    private bool walkedNearChair;
    public SphereCollider trigger;

    public float camRotationSpeed;
    public float camMoveSpeed;
    public Transform cutsceneTarget;
    public Transform cutscenePosition;
    public PlayerController controller;

    private bool sawScene1;
    private bool cam1Start;
    public GameObject scene1Cam;
    public GameObject collisionDetector;
    public Vector3 originalCam1Pos;

    private bool sawScene2;
    private bool cam2Start;
    public GameObject scene2Cam;
    public Vector3 originalCam2Pos;
    public Vector3 originalCam2Rot;
    public Transform blockTarget;

    private bool newView;
    public GameObject chair;
    private bool changedView;
    private bool returnToEmsy;
    private bool openedJournal;
    public RoomScript room;
    public UISwapScript uiScript;

    public GameObject options;
    public GameObject[] ui = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        //tutorialText.text = "Press <sprite=\"W 1\" index=\"0\"> <sprite=\"A 1\" index=\"0\"> <sprite=\"S 1\" index=\"0\"> <sprite=\"D 1\" index=\"0\"> to move";
        tutorialText.text = "Press 'W' 'A' 'S' 'D' to move";
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.bounds.Intersects(controller.gameObject.GetComponent<CapsuleCollider>().bounds)){
            walkedNearChair = true;
        }

        if (!walked)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
            {
                walked = true;
            }
        }
        else if(delay > 0.0f && !walkedNearChair)
        {
            delay -= freeTimeSpeed * Time.deltaTime;
        }
        else if (!sawScene1)
        {
            if (!cam1Start)
            {
                vantageManager.GetComponent<PerspectiveSwap>().enabled = false;
                cam1Start = true;
                originalCam1Pos = scene1Cam.transform.position; 
            }
            controller.lock_movement = true;
            if(collisionDetector != null)
            {
                collisionDetector.GetComponent<CamCollision>().enabled = false;
            }

            foreach (GameObject t in ui)
            {
                t.SetActive(!options.activeSelf);
            }

            scene1Cam.GetComponent<OldCameraController>().enabled = false;
            tutorialText.text = "These pulsating objects cannot be interacted with at first \n";

            Vector3 originalPosition = scene1Cam.transform.position;
            scene1Cam.transform.position = Vector3.MoveTowards(scene1Cam.transform.position, cutscenePosition.position, camMoveSpeed * Time.deltaTime);

            Vector3 lookDir = cutsceneTarget.position - scene1Cam.transform.position;
            Quaternion rot = Quaternion.LookRotation(lookDir, new Vector3(0,1,0));
            scene1Cam.transform.rotation = Quaternion.Lerp(scene1Cam.transform.rotation, rot, camRotationSpeed * Time.deltaTime);

            if (Vector3.Distance(originalPosition, scene1Cam.transform.position) == 0)
            {
                //tutorialText.text = "These pulsating objects cannot be interacted with at first \nPress <sprite=\"Space01\" index=\"0\"> to continue";
                tutorialText.text = "These pulsating objects cannot be interacted with at first \nPress 'Space' to continue";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sawScene1 = true;
                    scene1Cam.transform.position = originalCam1Pos;
                    scene1Cam.GetComponent<OldCameraController>().enabled = true;
                    vantageManager.GetComponent<PerspectiveSwap>().enabled = true;
                    controller.lock_movement = false;
                    if (collisionDetector != null)
                    {
                        collisionDetector.GetComponent<CamCollision>().enabled = true;
                    }
                }
            }
        }
        else if (!newView)
        {
            vantageManager.GetComponent<PerspectiveSwap>().swapEnabled = true;
            //tutorialText.text = "Press <sprite=\"Shift\" index=\"0\"> to get a new Perspective on the environment";
            tutorialText.text = "Press 'Shift' to peer through windows into the past";
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                newView = true;
            }
        }
        
        else if (chair.GetComponent<VisionObjectScript>().enabled)
        {
            if (vantageManager.GetComponent<PerspectiveSwap>().playerActive)
            {
                //tutorialText.text = "Press <sprite=\"Shift\" index=\"0\"> to return to the Perspective view";
                tutorialText.text = "Press 'Shift' to return to the Window view";
            } else
            {
                tutorialText.text = "Click on the pulsating object to activate it for little Emsy";
            }
        }
        // scene 2
        else if (!sawScene2)
        {
            if (!cam2Start)
            {
                cam2Start = true;
                vantageManager.GetComponent<PerspectiveSwap>().enabled = false;
                originalCam2Pos = scene2Cam.transform.position;
                originalCam2Rot = scene2Cam.transform.forward;
            }
            tutorialText.text = "This object can now be used by little Emsy \n";

            Vector3 originalPosition = scene2Cam.transform.position;
            scene2Cam.transform.position = Vector3.MoveTowards(scene2Cam.transform.position, cutscenePosition.position, 2f * camMoveSpeed * Time.deltaTime);
            
            foreach (GameObject t in ui)
            {
                t.SetActive(!options.activeSelf);
            }

            Vector3 lookDir = cutsceneTarget.position - scene2Cam.transform.position;


            Quaternion rot = Quaternion.LookRotation(lookDir, new Vector3(0, 1, 0));
            scene2Cam.transform.rotation = Quaternion.Lerp(scene2Cam.transform.rotation, rot, 1.5f*camRotationSpeed * Time.deltaTime);

            if (Vector3.Distance(originalPosition, scene2Cam.transform.position) == 0)
            {
                //tutorialText.text = "This object can now be used by little Emsy \n Press <sprite=\"Space01\" index=\"0\"> to continue";
                tutorialText.text = "This object can now be used by little Emsy \n Press 'Space' to continue";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sawScene2 = true;
                    scene2Cam.transform.position = originalCam2Pos;
                    scene2Cam.transform.forward = originalCam2Rot;
                    vantageManager.GetComponent<PerspectiveSwap>().enabled = true;
                }
            }
        }
        else if (!changedView)
        {
            uiScript.hideArrows = false;
            if (vantageManager.GetComponent<PerspectiveSwap>().playerActive)
            {
                //tutorialText.text = "Press <sprite=\"Shift\" index=\"0\"> to return to the Perspective view";
                tutorialText.text = "Press 'Shift' to return to the Window view";
            }
            else
            {
                vantageManager.GetComponent<PerspectiveSwap>().newViewEnabled = true;
                /*tutorialText.text = "Press  <sprite=\"A\" index=\"0\"> and <sprite=\"D\" index=\"0\">" 
                    + " or the arrows on the left and right to swap between views of the room";
                //*/
                tutorialText.text = "Press 'A' and 'D' or the arrows on the left and right to swap between views of the room";
                if (room.currentView != 0)
                {
                    changedView = true;
                }
                //*/
            }
            
        }
        else if(!returnToEmsy)
        {
            vantageManager.GetComponent<PerspectiveSwap>().swapEnabled = true;
            //tutorialText.text = "Press <sprite=\"Shift\" index=\"0\"> to return to the level";
            tutorialText.text = "Press 'Shift' to return to the level";
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                returnToEmsy = true;
            }
        }
        //Getting rid of the jorunal until we add it into the player controller
        else if (!openedJournal)
        {
            //tutorialText.text = "Press <sprite=\"Tab 1\" index=\"0\"> or <sprite=\"ESC\" index=\"0\"> to open the menu";
            tutorialText.text = "Press 'Tab' or 'Escape' to open the menu";
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                openedJournal = true;
            }
        }
        else
        {
            //tutorialText.text = "You've completed this tutorial";
            tutorialText.text = "";
            Destroy(gameObject, 2.5f);
        }
    }
}
