using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomOneDetection : MonoBehaviour
{
    public int blocksFound;
    public Text notifcationText;
    public Image blockUI;
    [Header("Make sure the endings of these images are the same as the endings of the gameobjects")]
    public Sprite[] blockImages;
    public Text collectUI;
    //Animation stuff i guess
    private Animator anim;
    private Rigidbody rb;
    public List<GameObject> itemsInReach = new List<GameObject>();

    private void Start()
    {
        collectUI.text = "";
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        blockUI.sprite = blockImages[0];
    }

    private void Update()
    {
        if (itemsInReach.Count > 0)
        {
            collectUI.text = "Left Click to collect";
        }
        else
        {
            collectUI.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Block"))
        {
            Transform objBase = other.transform.GetChild(other.transform.childCount - 1);
            Vector3 playerObjRay = objBase.position - transform.position;
            playerObjRay.y = 0.0f;
            float angle = Vector3.Angle(playerObjRay, transform.forward);
            bool facing = angle < 75f;

            if (GetComponent<PlayerController>().lock_movement || GetComponent<PlayerController>().manipulating || !facing)
            {
                //collectUI.text = "";
                if (itemsInReach.Contains(other.gameObject))
                {
                    itemsInReach.Remove(other.gameObject);
                }
                return;
            }

            //collectUI.text = "Left Click to collect";
            if (!itemsInReach.Contains(other.gameObject))
            {
                itemsInReach.Add(other.gameObject);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(other.gameObject);
                GetComponent<AudioSource>().Play();
                blocksFound++;
                //This wont stay on screen and won't disapear until you go to the box but sure
                notifcationText.text = "You have found Block Number: " + blocksFound;
                //blockUI.sprite = blockImages[blocksFound];
                //Dynamic Block Upater thingy
                //Just don't change the object names or whatever.
                foreach(Sprite s in blockImages)
                {
                    if(other.name.Substring(other.name.Length -2) == s.name.Substring(s.name.Length - 2))
                    {
                        blockUI.transform.GetChild(blocksFound - 1).GetComponent<Image>().sprite = s;
                    }
                }
                //collectUI.text = "";
                if (itemsInReach.Contains(other.gameObject))
                {
                    itemsInReach.Remove(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //collectUI.text = "";
        if (itemsInReach.Contains(other.gameObject))
        {
            itemsInReach.Remove(other.gameObject);
        }
    }

}
