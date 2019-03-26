using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmsyController : MonoBehaviour
{
    public Transform theCamera;
 
    public float movementSpeed;
    public float jump_power;
    private Transform myTransform;

    private float jumped;
    Animator anim;
    Rigidbody m_RigidBody;
 
 
    void Start()
    {
        myTransform = this.transform;
        //m_RigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("OnGround", true);
    }

    void Update()
    {
        // get inputs
        //if (Input.GetButtonDown("Space")){
        //    m_RigidBody.AddForce(new Vector3(0, 1, 0));
        //}
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        //float jumped = Input.GetAxis("Jumped");

        // get current position, then do calculations
        Vector3 moveVectorX = theCamera.forward * inputY  ;
        Vector3 moveVectorY = theCamera.right * inputX;
        //Vector3 moveVectorZ = theCamera.up * jumped;
        Vector3 moveVector = (moveVectorX + moveVectorY ).normalized * movementSpeed * Time.deltaTime;

        // update Character position
        myTransform.position = myTransform.position + new Vector3(moveVector.x, 0, moveVector.z);

        // and rotation
        myTransform.LookAt(myTransform.position + new Vector3(moveVector.x, 0, moveVector.z));

    }


}
