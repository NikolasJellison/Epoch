using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float input_speed;
    private float manip_speed = 1;
    private float speed;
    private Rigidbody rb;
    public LayerMask groundLayers;
    public float Jump_Force;
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
        //my_Camera = transform.GetChild(13).GetChild(0);

        speed = input_speed;
        crouched = false;
    }
    // Update is called once per frame
    void Update()
    {

        //Quick journal implementation
        if(Input.GetKeyDown(KeyCode.Tab))
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
            print("Locked");
            if (Input.GetKeyDown(KeyCode.E))
            {
                lock_movement = false;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hold_Onto"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lock_movement = true;
                transform.parent = other.GetComponent<Transform>().transform.parent.transform;
            }

        }
        else if (other.CompareTag("Move_Able") && !manipulating)
        {
            print("Movable Object is close");
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = transform;
                anim.SetBool("Manipulating", true);
                speed = manip_speed;
                manipulating = true;
            }
        }
        else if (other.CompareTag("Move_Able") && manipulating)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = null;
                anim.SetBool("Manipulating", false);
                speed = input_speed;
                manipulating = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Move_Able") && !manipulating)
        {
            print("Movable Object is close");
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = transform;
                anim.SetBool("Manipulating", true);
                speed = manip_speed;
                manipulating = true;
            }
        }
        else if (other.CompareTag("Move_Able") && manipulating)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.parent = null;
                anim.SetBool("Manipulating", false);
                speed = input_speed;
                manipulating = false;
            }

        }
    }


    void PlayerMovement()
    {
        if(crouched && Input.GetKeyDown(KeyCode.C))
        {
            crouched = false;
            anim.SetBool("Crouched",false);
            col.enabled = true;
            box_col.enabled = false;
            speed = input_speed;
        }else if (IsGrounded() && Input.GetKeyDown(KeyCode.C))
        {
            print("Crouched");
            anim.SetBool("Crouched", true);
            //my_Camera.position = Vector3.MoveTowards(my_Camera.position, )
            //iTween.MoveTo(transform.GetChild(13).GetChild(0).gameObject, transform.GetChild(13).GetChild(1).position, 5f);  
            speed = manip_speed;
            col.enabled = false;
            box_col.enabled = true;
            crouched = true;

        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !crouched)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Jump_Force, ForceMode.Impulse);
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if (manipulating)
        {
            hor = 0f;
        }
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    ver = ver * 2;
        //}


        Vector3 playermovement;
        playermovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;

        //if (ver < 0f && !crouched)
        //    playermovement = new Vector3(hor, 0f, ver) * speed / 2 * Time.deltaTime;
        //else
        //{
        //    ver = ver / 2;
        //    playermovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        //}
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
