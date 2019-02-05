using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomOneDetection : MonoBehaviour
{
    public int blocksFound;
    public Text notifcationText;

    //Animation stuff i guess
    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Destroy(other.gameObject);
            blocksFound++;
            //This wont stay on screen and won't disapear until you go to the box but sure
            notifcationText.text = "You have found Block Number: " + blocksFound;
        }
    }

}
