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
    // Start is called before the first frame update
    void Start()
    {
        collectUI.text = "";    
    }

    // Update is called once per frame
    void Update()
    {
        pageUI.sprite = pageImages[pagesFound];
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
            if (GetComponent<PlayerController>().lock_movement)
            {
                collectUI.text = "";
                return;
            }
            collectUI.text = "Left Click to collect";
            if (Input.GetMouseButtonDown(0))
            {
                subPage();
                GetComponent<AudioSource>().Play();
                //AkSoundEngine.PostEvent("Acquisition", gameObject);
                print(pageImages[pagesFound].name);
                other.gameObject.SetActive(false);
                collectUI.text = "";
            } 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        collectUI.text = "";
    }
}
