using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Script : MonoBehaviour
{
    public int pagesFound;
    public Image pageUI;
    public Text collectUI;
    [Header("0 blocks first, then go up to max")]
    public Sprite[] pageImages;
    public List<GameObject> itemsInReach = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        collectUI.text = "";    
    }

    // Update is called once per frame
    void Update()
    {
        pageUI.sprite = pageImages[pagesFound];
        if(itemsInReach.Count > 0)
        {
            collectUI.text = "Left Click to collect";
        } else
        {
            collectUI.text = "";
        }
    }
    public void subPage()
    {
        print("Got a page");
        ++pagesFound;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Pages"))
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

            if (!itemsInReach.Contains(other.gameObject))
            {
                itemsInReach.Add(other.gameObject);
            }
            //collectUI.text = "Left Click to collect";
            if (Input.GetMouseButtonDown(0))
            {
                subPage();
                GetComponent<AudioSource>().Play();
                print(pageImages[pagesFound].name);
                other.gameObject.SetActive(false);
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
