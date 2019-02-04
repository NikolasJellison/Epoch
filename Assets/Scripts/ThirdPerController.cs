using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerController : MonoBehaviour
{
    public Transform theCamera;
 
    private float movementSpeed = 5.0f;
    private Transform myTransform;
 
 
    void Start()
    {
            myTransform = this.transform;
            }

    void Update()
    {
        // get inputs
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // get current position, then do calculations
        Vector3 moveVectorX = theCamera.forward * inputY;
        Vector3 moveVectorY = theCamera.right * inputX;
        Vector3 moveVector = (moveVectorX + moveVectorY).normalized * movementSpeed * Time.deltaTime;

        // update Character position
        myTransform.position = myTransform.position + new Vector3(moveVector.x, 0.0f, moveVector.z);

        // and rotation
        myTransform.LookAt(myTransform.position + new Vector3(moveVector.x, 0.0f, moveVector.z));

    }
}
