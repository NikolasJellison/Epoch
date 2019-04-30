using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //need to make this public to for Worlds space text (temp fix)
    public bool lock_movement;
    public bool manipulating;
    public GameObject journalUI;
    public GameObject optionsPanel;
    public GameObject[] cursorImages;
    //private Transform my_Camera;
    // Updated object pushing
    public List<GameObject> moveableCandidates;
    public GameObject heldObject;
    public GameObject crawlerCandidate;
    public float bestAngle;
    public Text actionUI;

    //public Camera cam;
    //public float objectScale = 1.0f;
    //private Vector3 initialScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        box_col = GetComponent<BoxCollider>();
        destination = new Vector3(0, 0, 0);
        //my_Camera = transform.GetChild(13).GetChild(0);

        speed = input_speed;
        if(actionUI != null)
        {
            actionUI.text = "";
        }
        
        crouched = false;

        //initialScale = transform.localScale;

        //if(cam == null)
        //{
        //    cam = Camera.main;
        //}
    }


    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        // This is to 100% make sure we hold objects we are in contact with
        if (moveableCandidates.Count > 0)
        {
            List<GameObject> removeList = new List<GameObject>();

            foreach (GameObject moveable in moveableCandidates)
            {
                BoxCollider[] colliders = moveable.GetComponents<BoxCollider>();
                bool inContact = false;
                foreach (BoxCollider collider in colliders)
                {
                    
                    if (collider.isTrigger)
                    {
                        if (gameObject.GetComponent<CapsuleCollider>().bounds.Intersects(collider.bounds))
                        {
                            
                            inContact = true;
                            break;
                        }
                    }

                }
                if (!inContact)
                {
                    removeList.Add(moveable);
                }
                
            }

            foreach (GameObject lost in removeList)
            {
                if (moveableCandidates.Contains(lost))
                {
                    moveableCandidates.Remove(lost);
                }
            }
        }
        //*/

        //Quick journal implementation
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            JournalInteract();
        }
        
        if (!lock_movement)
        {
            //print("Not Locked");
            if (crawlerCandidate != null && !crouched && !manipulating)
            {
                if (actionUI != null)
                {
                    actionUI.text = "'E' to crawl";
                }

                if (Input.GetKeyDown(KeyCode.E) && IsGrounded())
                {
                    transform.position = crawlerCandidate.transform.position;
                    transform.rotation = crawlerCandidate.transform.rotation;
                    destination = crawlerCandidate.transform.GetChild(0).transform.position;
                    col.enabled = false;
                    box_col.enabled = true;
                    crouched = true;
                    anim.SetBool("Crouched", true);
                }
            }
            // || manipulating JUST in case we leave a held object's collider   
            else if (moveableCandidates.Count > 0 || manipulating)
            {


                if (manipulating) // you're holding an object
                {
                    if (actionUI != null)
                    {
                        actionUI.text = "'E' to let go";
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        heldObject.transform.parent = null;
                        heldObject = null;
                        speed = input_speed;
                        manipulating = false;
                        anim.SetBool("Manipulating", false);
                    }
                }
                else
                {
                    Vector3 forwardVec = transform.forward;
                    forwardVec.y = 0.0f;

                    GameObject bestObject = moveableCandidates[0];
                    Transform objBase = bestObject.transform.GetChild(bestObject.transform.childCount - 1);

                    Vector3 playerObjRay = objBase.position - transform.position;
                    playerObjRay.y = 0.0f;


                    bestAngle = Vector3.Angle(playerObjRay, transform.forward);
                    for (int i = 1; i < moveableCandidates.Count; ++i)
                    {
                        objBase = moveableCandidates[i].transform.GetChild(moveableCandidates[i].transform.childCount - 1);
                        playerObjRay = objBase.position - transform.position;
                        playerObjRay.y = 0.0f;
                        float angle = Vector3.Angle(playerObjRay, transform.forward);
                        if (angle < bestAngle)
                        {
                            bestObject = moveableCandidates[i];
                            bestAngle = angle;
                        }
                    }
                    objBase = bestObject.transform.GetChild(bestObject.transform.childCount - 1);
                    playerObjRay = objBase.position - transform.position;

                    Debug.DrawRay(transform.position, playerObjRay, Color.green);
                    Vector3.Angle(playerObjRay, transform.forward);
                    if (bestAngle > 45.0f)
                    {
                        bestObject = null;
                        if (actionUI != null)
                        {
                            actionUI.text = "";
                        }

                    }
                    else
                    {
                        if (actionUI != null)
                        {
                            actionUI.text = "'E' to move";
                        }

                    }

                    if (bestObject != null && Input.GetKeyDown(KeyCode.E))
                    {
                        // Choose what moveableCandidate to choose.
                        // For now, just choose the first one 
                        heldObject = bestObject;
                        // heldObject = moveableCandidates[0];
                        heldObject.transform.parent = transform;
                        speed = manip_speed;
                        manipulating = true;
                        anim.SetBool("Manipulating", true);
                    }
                }
            }
            else
            {
                if (actionUI != null)
                {
                    actionUI.text = "";
                }

            }
            PlayerMovement();

        }
        else
        {
            if(actionUI != null)
            {
                actionUI.text = "";
            }
            //lock_movement &= !Input.GetKeyDown(KeyCode.E);
            anim.SetFloat("Velocity_X", 0);
            anim.SetFloat("Velocity_Y", 0);
        }

        //Plane plane = new Plane(cam.transform.forward, cam.transform.position);
        //float dist = plane.GetDistanceToPoint(transform.position);
        //transform.localScale = initialScale * dist * objectScale;

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crawler"))
        {
            crawlerCandidate = other.gameObject;
        }

        if (other.CompareTag("Move_Able"))
        {
            GameObject moveable = other.gameObject;
            if (!moveableCandidates.Contains(moveable))
            {
                moveableCandidates.Add(moveable);
            }
        }
    }
    //*/

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crawler"))
        {
            crawlerCandidate = null;
        }

        if (other.CompareTag("Move_Able"))
        {
            GameObject moveable = other.gameObject;
            if (moveableCandidates.Contains(moveable))
            {
                moveableCandidates.Remove(moveable);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // This shouldn't need t be here, but eh
        if (other.CompareTag("Crawler") && crawlerCandidate == null)
        {
            crawlerCandidate = other.gameObject;
        }

        if (other.CompareTag("Move_Able"))
        {
            GameObject moveable = other.gameObject;
            if (!moveableCandidates.Contains(moveable))
            {
                moveableCandidates.Add(moveable);
            }
        }
        /*
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
        //*/
        /*
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
        //*/
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
                crawlerCandidate = null;
                crouched = false;
                anim.SetBool("Crouched", false);
                col.enabled = true;
                box_col.enabled = false;
                speed = input_speed;
            }
        }

        

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

    public void Smash(){
    	anim.SetTrigger("Smash");
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
