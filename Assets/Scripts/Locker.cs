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

    private bool isOpen = false;
        //"Detecting" when the locker is closed for the first time
    private bool closedFirst;

    private void Start()
    {
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
        }
    }
}
