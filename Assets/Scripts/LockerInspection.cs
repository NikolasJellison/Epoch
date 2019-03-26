using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInspection : MonoBehaviour
{
    public Transform inspectionLocation;
    private Vector3 originalLocation;
    private Vector3 destination;
    [Header("CHECK THIS IF THIS IS THE LAPTOP THANKS!")]
    public bool isLaptop;
    public bool isClue;
    [Header("This is the locker script")]
    public Locker locker;
    //Clue stuff
    public Manager gameManager;
    [Header("1 is first ... 4 is last")]
    public int whichClue;

    public float speed = 2;
    private float step;
    //Conditional on player script to make sure the player isn't inspecting something else
    //Goes Here
    private bool isMoving;
    private bool isInspected;




    private void Start()
    {
        originalLocation = transform.position;
        destination = originalLocation;
    }

    private void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        if (!locker.isOpen)
        {
            destination = originalLocation;
        }
    }

    private void OnMouseDown()
    {

        if (isInspected)
        {
            //Return
            destination = originalLocation;
            locker.currentInspectingObject = false;
        }
        else if (locker.currentInspectingObject == false)
        {
            //Inspect
            destination = inspectionLocation.position;
            if (isLaptop)
            {
                Destroy(gameObject);
                //Send to gamemanager letting it know that the laptop has been found
                //Let gamemanager deal with the notification text 
            }
            else if (isClue)
            {
                gameManager.writeClue(whichClue);
            }
            locker.currentInspectingObject = true;
        }

        isInspected = !isInspected;
    }

    //because we nolonger use mouse
    public void OpenLocker()
    {
        if (isInspected)
        {
            //Return
            destination = originalLocation;
            locker.currentInspectingObject = false;
        }
        else if (locker.currentInspectingObject == false)
        {
            //Inspect
            destination = inspectionLocation.position;
            if (isLaptop)
            {
                Destroy(gameObject);
                //Send to gamemanager letting it know that the laptop has been found
                //Let gamemanager deal with the notification text 
            }
            else if (isClue)
            {
                gameManager.writeClue(whichClue);
            }
            locker.currentInspectingObject = true;
        }

        isInspected = !isInspected;
    }
}
