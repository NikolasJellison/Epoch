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

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector3.up * Jump_Force, ForceMode.Impulse);
        }

    }

    void PlayerMovement()
    {
        //if(Input.GetKeyUp(KeyCode.LeftShift) && anim.GetBool("Running"))
        //{
        //    speed = speed / 2;
        //    anim.SetBool("Running", false);
        //}

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

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
