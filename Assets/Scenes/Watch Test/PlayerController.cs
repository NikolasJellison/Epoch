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
        PlayerMovement();
        if (IsGrounded())
        {
            anim.SetBool("Jumping", false);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jumping", true);
            rb.AddForce(Vector3.up * Jump_Force, ForceMode.Impulse);
        }

    }

    void PlayerMovement()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift) && anim.GetBool("Running"))
        {
            speed = speed / 2;
            anim.SetBool("Running", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true);
            speed = speed * 2;
        }
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if (ver == 0f && hor == 0f)
        {
            anim.SetBool("Moving", false);
        }
        else
            anim.SetBool("Moving", true);

        anim.SetFloat("Direction", ver);
        Vector3 playermovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playermovement, Space.Self);
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
                new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .8f, groundLayers);
    }
}
