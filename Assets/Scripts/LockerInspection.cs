using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInspection : MonoBehaviour
{
    public Transform inspectionLocation;
    private Vector3 originalLocation;
    private Vector3 destination;

    public float speed = 2;
    private float step;
    //Conditional on player script to make sure the player isn't inspecting something else
    //Ggoes Here
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
    }

    private void OnMouseDown()
    {
        switch (isInspected)
        {
            case true:
                //Return
                destination = originalLocation;
                break;

            case false:
                //Inspect
                destination = inspectionLocation.position;
                break;
        }

        isInspected = !isInspected;
    }
}
