using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryClimb : MonoBehaviour
{
    private Vector3 tempPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && tempPosition != Vector3.zero)
        {
            transform.position = tempPosition;
            tempPosition = Vector3.zero;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Name: " + collision.gameObject.name);
        Debug.Log("Bounds: " + collision.gameObject.GetComponent<MeshRenderer>().bounds);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag != "Not-ClimbAble" )
        {
            //If You want to put any indicator that you can climb put it in here (then remove the text in OnCollisionExit
            tempPosition = transform.position;
            tempPosition.y = collision.gameObject.GetComponent<MeshRenderer>().bounds.max.y;
            //Debug.Log(collision.gameObject.GetComponent<MeshRenderer>().bounds.SqrDistance(transform.position));
            //Debug.Log(collision.gameObject.GetComponent<MeshRenderer>().bounds.ClosestPoint(transform.position));
            tempPosition.x = collision.gameObject.GetComponent<MeshRenderer>().bounds.ClosestPoint(transform.position).x;
            tempPosition.z = collision.gameObject.GetComponent<MeshRenderer>().bounds.ClosestPoint(transform.position).z;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        tempPosition = Vector3.zero;
    }
}
