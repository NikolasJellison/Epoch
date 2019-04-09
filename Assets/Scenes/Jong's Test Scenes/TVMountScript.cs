using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVMountScript : MonoBehaviour
{
    public Transform tvPos;
    public Transform pivot;
    public float rotate;
    public float speed;
    public float rotateLimit;
    public Text moveUI;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        moveUI.text = "";
        rotate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(start && rotate < rotateLimit)
        {
            pivot.Rotate(0, 0, -speed*Time.deltaTime);
            rotate += speed*Time.deltaTime;
        }
        
        transform.position = tvPos.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (start)
        {
            moveUI.text = "";
            return;
        }
        moveUI.text = "'E' to move aside";
        if (Input.GetKeyDown(KeyCode.E))
        {
            start = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        moveUI.text = "";
    }
}
