using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene1 : MonoBehaviour
{
    public GameObject player;
    private CutsceneCameraController cameraController;
    public Camera playerCam;
    public Camera cutSceneCam;
    public GameObject computerScreenCanvas;
    [Header("Anything that needs to get deleted once the room has dissolved")]
    public GameObject[] delete;
    [Header("These need to have the disolve shader on them")]
    public MeshRenderer[] objectsToDisolve;
    private float dissolveCounter;
    private bool dissolve;
    private bool falling;
    private bool isWalking;
    private GameObject camera1A;

    public Transform landing;
    public Transform windowLook;
    public float fallSpeed = 3;

    private void Start()
    {
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        cameraController = player.GetComponentInChildren<CutsceneCameraController>();
        camera1A = GameObject.Find("Camera-Cutscene1A");
        camera1A.GetComponent<Camera>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        //Debug
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            EnterDesktop();
            //So if you bypass the beginning animation with the 'debug' key, you will still be able to click on the exit button on the computer screen
            cameraController.notificationText.gameObject.transform.parent.gameObject.SetActive(false);
        }

        //There is a better way but idk how to make the fill continuous
        if (dissolve)
        {
            dissolveCounter += (Time.deltaTime / 3);

            if (dissolveCounter >= 1)
            {
                dissolve = false;
                dissolveCounter = 1;

                foreach(GameObject obj in delete)
                {
                    Destroy(obj);
                }

                //Start Fall
                cameraController.canMove = false;
                cameraController.StartFall();
                falling = true;
            }

            foreach (MeshRenderer obj in objectsToDisolve)
            {
                obj.material.SetFloat("_DissolveValue", dissolveCounter);
                //Debug.Log("Dissovlecounter: " + dissolveCounter);
            }
        }

        if(falling){
            player.transform.position = Vector3.MoveTowards(player.transform.position, landing.position, fallSpeed * Time.deltaTime);
            if (Vector3.Distance(player.transform.position, landing.position) < .01)
            {
                falling = false;
                cameraController.StartWalk();
                isWalking = true;
                //Change active cameras
                playerCam.enabled = false;
                //Activate the camera anim
                camera1A.GetComponent<Animator>().SetTrigger("StartMove");
                camera1A.GetComponent<Camera>().enabled = true;
            }
        }

        if (isWalking)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, windowLook.position, fallSpeed * Time.deltaTime);
            if (Vector3.Distance(player.transform.position, windowLook.position) < .01)
            {
                isWalking = false;
                cameraController.StartLook(); 
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    EnterDesktop();
    //}

    public void EnterDesktop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        playerCam.enabled = false;
        cutSceneCam.enabled = true;
    }

    public void ExitDesktop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        dissolve = true;
        computerScreenCanvas.SetActive(false);
    }
}
