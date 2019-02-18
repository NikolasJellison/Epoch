using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    private Rigidbody rb;
    public LayerMask groundLayers;
    public float Jump_Force = 3;
    private CapsuleCollider col;
    private bool lock_movement = false;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();



    }
    // Update is called once per frame
    void Update()
    {
        if (!lock_movement)
        {
            print("Not Locked");
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

    }


    void PlayerMovement()
    {
        //if(Input.GetKeyUp(KeyCode.LeftShift) && anim.GetBool("Running"))
        //{
        //    speed = speed / 2;
        //    anim.SetBool("Running", false);
        //}
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Jump_Force, ForceMode.Impulse);
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical")/2;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ver = ver * 2;
        }

        anim.SetFloat("Velocity_X", hor);
        anim.SetFloat("Velocity_Y", ver);
        Vector3 playermovement;
        if (ver < 0f)
            playermovement = new Vector3(hor, 0f, ver) * speed/2 * Time.deltaTime;
        else
            playermovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;

        transform.Translate(playermovement, Space.Self);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
                new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .8f, groundLayers);
    }
}
