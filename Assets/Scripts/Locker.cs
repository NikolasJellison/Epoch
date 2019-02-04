using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Animator anim;
    [Header("Cameras. NOTE:make sure to disable the camera component on the locker")]
        //Using gameobject instead of cameras so we don't have to disabled audio listeners as well
    public GameObject playerCamera;
    public GameObject LockerCamera;
    [Header("Sounds")]
    public AudioSource lockerOpening;
    public AudioSource lockerClose;

    [HideInInspector]public bool isOpen = false;
    [HideInInspector]public bool currentInspectingObject;
        //"Detecting" when the locker is closed for the first time
    public bool closedFirst;

    private void Start()
    {
        closedFirst = false;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnMouseDown()
    {

        if (playerCamera.activeSelf)
        {
            //Opening
            anim.SetTrigger("LockerActivate");
            playerCamera.SetActive(false);
            LockerCamera.SetActive(true);
            lockerOpening.Play();
            isOpen = true;
        }
        else if (!playerCamera.activeSelf && isOpen)
        {
            //Closing
            anim.SetTrigger("LockerActivate");
            playerCamera.SetActive(true);
            LockerCamera.SetActive(false);
            lockerClose.Play();
            isOpen = false;
            closedFirst = true;
        }
    }
}
