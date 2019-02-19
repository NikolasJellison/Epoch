using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startToEndPoint2 : MonoBehaviour
{
    //Home and Desination
    [SerializeField]
    Transform endPoint;
    private Vector3 startPosition;
    private Vector3 destination;

    //Movement speed
    public float moveSpeed = 2;

    public float timeBank = 3;
    private float timer = 0;

    //
    private bool isMousePressed = false;
    //access from time bank script
    [HideInInspector] public bool moveToDestination;
    private bool isMovingBack = false;

    //Raycast stuff for blockers
    public float detectionRange = .5f;
    private float originalMoveSpeed;


     void Start()
    {
        //Wherever the object is at the start of runtime, that is its "home"
        startPosition = transform.position;
        //default its destination to the start
        destination = startPosition;
        //Going to set moveSpeed to 0 when object is blocked, need this to reset it back
        //Probably a better way to do this
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        /*
        if (Input.GetMouseButtonDown(1))
        {
            isMousePressed = true;
        }
        */

        if (moveToDestination && transform.position != endPoint.position)
        {
            //Move to destination
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, moveSpeed * Time.deltaTime);
        }
        else if(!moveToDestination && transform.position != startPosition)
        {
            //move home
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }

        //Getting the time from the time_bank script now
        /*
        if (transform.position == endPoint.position)
        {
            moveToDestination = false;
            timer += Time.deltaTime;
        }
        
        if (timer > timeBank)
        {
            isMovingBack = true;
            if (transform.position != startPosition)
            {
                //Move to Home
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            }
            if(transform.position == startPosition)
            {
                timer = 0;
                isMovingBack = false;
            }
        }
        */

        //Going to get direction of movement, then raycast and see if something is in front of the direction it is moving

        //Debug here to show where the object's home and destiation are (only shows up in 'scene view' while in runtime
        //[Blue] = Home (startposition)
        //[Red] = Destination (endposition)
        Debug.DrawRay(transform.position, startPosition - transform.position, Color.blue);
        Debug.DrawRay(transform.position, endPoint.position - transform.position, Color.red);

        RaycastHit hit;
        if (!moveToDestination)
        {
            if (Physics.Raycast(transform.position, startPosition - transform.position, out hit, detectionRange))
            {
                //Moving to Home
                Debug.Log(transform.name + " is stopping because I hit an object with name:" + hit.transform.name + " while trying to move to my home at:" + startPosition);
                moveSpeed = 0;
                //Might need to add some kind of check to make sure the object is only stopping when blocked by the correct things
                //So maybe a compareTag here or something
            }
            else
            {
                moveSpeed = originalMoveSpeed;
            }
        }
        else if (moveToDestination)
        {
            if (Physics.Raycast(transform.position, endPoint.position - transform.position, out hit, detectionRange))
            {
                //Moving to Destination
                Debug.Log(transform.name + " is stopping because I hit an object with name:" + hit.transform.name + " while trying to move to my destination, named:" + endPoint.name + ". Which is located at:" + endPoint.position);
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = originalMoveSpeed;
            }
        }
    }
}
