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

    private bool isOpen;
        //"Detecting" when the locker is closed for the first time
    private bool closedFirst;

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void OnMouseDown()
    {
        anim.SetTrigger("LockerActivate");
        

        switch (playerCamera.activeSelf)
        {
            //Opening
            case true:
                playerCamera.SetActive(false);
                LockerCamera.SetActive(true);
                lockerOpening.Play();
                break;
            //Closing
            case false:
                playerCamera.SetActive(true);
                LockerCamera.SetActive(false);
                if (!closedFirst)
                {
                    //Send some function to make the tiles fill in 
                    //Might need to do more changes because there are different parts that need to get filled in
                }
                closedFirst = true;
                lockerClose.Play();
                break;
        }

        isOpen = !isOpen;
    }
}
