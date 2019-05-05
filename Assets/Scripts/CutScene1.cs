using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public AudioSource ambiance;
    public Transform landing;
    public Transform windowLook;
    public float fallSpeed = 3;
    public AudioSource radio;
    private bool dissolveLR;
    public TextMeshProUGUI sadText;
    

    private void Start()
    {
        sadText.text = "";
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
        if (cutSceneCam.isActiveAndEnabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
            foreach (GameObject obj in delete)
            {
                obj.SetActive(false);
            }
            sadText.text = "";
            //Reset Camera
            playerCam.gameObject.transform.parent.rotation = targetOGRotation;

            dissolveCounter += (Time.deltaTime / 3);

            if (dissolveCounter >= 1)
            {
                dissolve = false;
                dissolveCounter = 1;

                

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
        player.GetComponent<PlayerController>().enabled = false;
        playerCam.enabled = false;
        cutSceneCam.enabled = true;
    }

    public void ExitDesktop()
    {
        sadText.gameObject.transform.parent.gameObject.SetActive(true);
        sadText.text = "It's always been like this...";
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        //dissolve = true;
        computerScreenCanvas.SetActive(false);
        radio.Play();
        ambiance.Stop();
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
