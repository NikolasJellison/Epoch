using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startToEndPoint : MonoBehaviour
{
    [SerializeField]
    Transform movingObject;

    [SerializeField]
    Transform startPoint;

    [SerializeField]
    Transform endPoint;

    public float moveSpeed = 2;

    public float timeBank = 3;
    private float timer = 0;

    private Vector3 direction;
    private Transform destination;
    private bool isMousePressed = false;
    private bool isMovingBack = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isMousePressed = true;
        }
        if (isMousePressed == true && isMovingBack == false)
        {
            if(movingObject.position != endPoint.position)
            {
                movingObject.position = Vector3.MoveTowards(movingObject.position, endPoint.position, moveSpeed * Time.deltaTime);
            }
        }
        if (movingObject.position == endPoint.position)
        {
            isMousePressed = false;
            timer += Time.deltaTime;
        }
        if (timer > timeBank)
        {
            isMovingBack = true;
            if (movingObject.position != startPoint.position)
            {
                movingObject.position = Vector3.MoveTowards(movingObject.position, startPoint.position, moveSpeed * Time.deltaTime);
            }
            if(movingObject.position == startPoint.position)
            {
                timer = 0;
                isMovingBack = false;
            }
        }
    }
}
