//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EmsyCharacter : MonoBehaviour
//{
//    Rigidbody m_Rigidbody;
//    Animator m_Animator;
//    bool m_IsGrounded;
//    float m_CapsuleHeight;
//    Vector3 m_CapsuleCenter;
//    CapsuleCollider m_Capsule;
//    bool m_Crouching; 

//    // Start is called before the first frame update
//    void Start()
//    {
//        m_Animator = GetComponent<Animator>();
//        m_Rigidbody = GetComponent<Rigidbody>();
//        m_Capsule = GetComponent<CapsuleCollider>();
//        m_CapsuleHeight = m_Capsule.height;
//        m_CapsuleCenter = m_Capsule.center;

//        //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
//        //m_OrigGroundCheckDistance = m_GroundCheckDistance;

//    }

//    public void Move(Vector3 move, bool crouch, bool jump)
//    {

//        // convert the world relative moveInput vector into a local-relative
//        // turn amount and forward amount required to head in the desired
//        // direction.
//        if (move.magnitude > 1f) move.Normalize();
//        move = transform.InverseTransformDirection(move);
//        CheckGroundStatus();
//        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
//        m_TurnAmount = Mathf.Atan2(move.x, move.z);
//        m_ForwardAmount = move.z;

//        ApplyExtraTurnRotation();

//        // control and velocity handling is different when grounded and airborne:
//        if (m_IsGrounded)
//        {
//            HandleGroundedMovement(crouch, jump);
//        }
//        else
//        {
//            HandleAirborneMovement();
//        }

//        ScaleCapsuleForCrouching(crouch);
//        PreventStandingInLowHeadroom();

//        // send input and other state parameters to the animator
//        UpdateAnimator(move);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
