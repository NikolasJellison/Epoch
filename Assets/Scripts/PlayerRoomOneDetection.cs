using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomOneDetection : MonoBehaviour
{
    public int blocksFound;
    public Text notifcationText;
    public Image blockUI;
    [Header("0 blocks first, then go up to 5/5")]
    public Sprite[] blockImages;
    public Text collectUI;
    //Animation stuff i guess
    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        collectUI.text = "";
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        blockUI.sprite = blockImages[0];
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            collectUI.text = "Left click to collect";
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(other.gameObject);
                //AkSoundEngine.PostEvent("Acquisition", gameObject);
                blocksFound++;
                //This wont stay on screen and won't disapear until you go to the box but sure
                notifcationText.text = "You have found Block Number: " + blocksFound;
                blockUI.sprite = blockImages[blocksFound];
                collectUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collectUI.text = "";
    }

}
