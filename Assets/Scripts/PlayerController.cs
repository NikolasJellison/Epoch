using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float input_speed;
	
    public float manip_speed;
    private float speed;
    private Vector3 destination;
    private Rigidbody rb;
    public LayerMask groundLayers;
    public float Jump_Force = 3;
    private CapsuleCollider col;
    private BoxCollider box_col;
    //private bool lock_movement;
    private Animator anim;
    //private bool manipulating;
    private bool crouched;
    //Quick Journal Stuff
    //need to make this public to for Worlsd space text (temp fix)
    public bool lock_movement;
    public bool manipulating;
    public GameObject journalUI;
    public GameObject optionsPanel;
    public GameObject[] cursorImages;
    //private Transform my_Camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        box_col = GetComponent<BoxCollider>();
        destination = new Vector3(0, 0, 0);
        //my_Camera = transform.GetChild(13).GetChild(0);

        speed = input_speed;
        crouched = false;
    }
    // Update is called once per frame
    void Update()
    {

        //Quick journal implementation
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            JournalInteract();
        }

        if (!lock_movement)
        {
            //print("Not Locked");

                PlayerMovement();

        }
        else
        {
            lock_movement &= !Input.GetKeyDown(KeyCode.E);
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Crawler") && IsGrounded())
        {
            print("Inside crawler");
            if (Input.GetKeyDown(KeyCode.C) && !manipulating)
            {
                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;
                destination = other.transform.GetChild(0).transform.position;
                col.enabled = false;
                box_col.enabled = true;
                crouched = true;
                anim.SetBool("Crouched", true);
            }
        }

        //Changed COmpare tag to tag Contains for temp Climb script
        if (other.tag.Contains("Move_Able") && !manipulating)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = transform;
                speed = manip_speed;
                manipulating = true;
                anim.SetBool("Manipulating", true);
            }
        }
        else if (other.tag.Contains("Move_Able") && manipulating)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = null;
                speed = input_speed;
                manipulating = false;
                anim.SetBool("Manipulating", false);

            }

        }
    }




    void PlayerMovement()
    {

        //if (crouched && Input.GetKeyDown(KeyCode.C))
        //{
        //    crouched = false;
        //    anim.SetBool("Crouched",false);
        //    col.enabled = true;
        //    box_col.enabled = false;
        //    speed = input_speed;
        //}else if (IsGrounded() && Input.GetKeyDown(KeyCode.C))
        //{
        //    anim.SetBool("Crouched", true);
        //    speed = manip_speed;
        //    col.enabled = false;
        //    box_col.enabled = true;
        //    crouched = true;

        //}
        if (crouched)
        {
            if(Mathf.Abs(transform.position.x - destination.x) < 1f && Mathf.Abs(transform.position.z - destination.z) < 1f)
            {
                crouched = false;
                anim.SetBool("Crouched", false);
                col.enabled = true;
                box_col.enabled = false;
                speed = input_speed;
            }
        }

        //COMMENTING OUT JUMP -nik (sorry)
        //if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !crouched)
        //{
        //    anim.SetTrigger("Jump");
        //    rb.AddForce(Vector3.up * Jump_Force, ForceMode.Impulse);
        //}

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    ver = ver * 2;
        //}


        Vector3 playermovement;

        if (crouched)
        {
            hor = 0f;
            ver = .5f;
        }

        playermovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        
        anim.SetFloat("Velocity_X", hor);
        anim.SetFloat("Velocity_Y", ver);

        transform.Translate(playermovement, Space.Self);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
                new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .8f, groundLayers);
    }

    public bool IsCrouched()
    {
        return crouched;
    }

    public bool IsManip()
    {
        return manipulating;
    }

    public void JournalInteract()
    {
        if(optionsPanel.activeSelf == false)
        {
            lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach(GameObject img in cursorImages)
            {
                img.SetActive(false);
            }
        }
        else
        {
            lock_movement = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            foreach (GameObject img in cursorImages)
            {
                img.SetActive(true);
            }
        }
        optionsPanel.SetActive(!optionsPanel.activeSelf);
        journalUI.SetActive(!journalUI.activeSelf);
    }
}
