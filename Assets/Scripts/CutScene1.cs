﻿using System.Collections;
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
    public float screenDelay = 6;
    [Header("Anything that needs to get deleted once the room has dissolved")]
    public GameObject[] delete;
    public GameObject[] appearLR;
    [Header("These need to have the disolve shader on them")]
    public MeshRenderer[] objectsToDisolve;
    public MeshRenderer[] objectsToDissolveLR;
    private float dissolveCounter;
    [HideInInspector]public bool dissolve;
    private bool falling;
    private bool isWalking;
    private GameObject camera1A;
    private Quaternion targetOGRotation;

    public Transform landing;
    public Transform windowLook;
    public float fallSpeed = 3;

    private bool dissolveLR;

    private void Start()
    {
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        cameraController = player.GetComponentInChildren<CutsceneCameraController>();
        camera1A = GameObject.Find("Camera-Cutscene1A");
        camera1A.GetComponent<Camera>().enabled = false;

        foreach (GameObject obj in appearLR)
        {
            obj.SetActive(false);
        }
        //Allows the player to look around for a bit
        //Then we bring up the message and we begin
        computerScreenCanvas.SetActive(false);
        cameraController.canMove = true;
        StartCoroutine(DelayComputer());

        //Store orginal rotation of target to get a good view for the crying
        targetOGRotation = playerCam.gameObject.transform.parent.rotation;
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
            //Reset Camera
            playerCam.gameObject.transform.parent.rotation = targetOGRotation;

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
        if (dissolveLR)
        {
            DissolveLR();
        }

        if(falling){
            player.transform.position = Vector3.MoveTowards(player.transform.position, landing.position, fallSpeed * Time.deltaTime);
            if (Vector3.Distance(player.transform.position, landing.position) < .01)
            {
                falling = false;
                //cameraController.StartWalk();
                //isWalking = true;
                //Change active cameras
                playerCam.enabled = false;
                ////Activate the camera anim
                //camera1A.GetComponent<Animator>().SetTrigger("StartMove");
                camera1A.GetComponent<Camera>().enabled = true;
                dissolveLR = true;
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

    private void DissolveLR()
    {
        //Pretty much the reverse of what happened at the beginning
        dissolveCounter -= (Time.deltaTime / 5);

        if (dissolveCounter <= 0)
        {
            dissolveLR = false;
            //This needs to be slightly below 0 because otherwise there is a small part of the shader that is still emissive
            dissolveCounter = -.01f;


            cameraController.StartWalk();
            isWalking = true;
            //Change active cameras
            playerCam.enabled = false;
            //Activate the camera anim
            camera1A.GetComponent<Animator>().SetTrigger("StartMove");
            camera1A.GetComponent<Camera>().enabled = true;

            foreach(GameObject obj in appearLR)
            {
                obj.SetActive(true);
            }
        }

        foreach (MeshRenderer obj in objectsToDissolveLR)
        {
            obj.material.SetFloat("_DissolveValue", dissolveCounter);
        }
    }

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
        //dissolve = true;
        computerScreenCanvas.SetActive(false);
        player.GetComponent<Animator>().SetTrigger("StartCry");
        cameraController.canMove = false;
        //Good view for cry
        playerCam.gameObject.transform.parent.rotation = targetOGRotation;
    }

    private IEnumerator DelayComputer()
    {
        yield return new WaitForSeconds(screenDelay);
        computerScreenCanvas.SetActive(true);
        cameraController.screenOn = true;
    }
}
