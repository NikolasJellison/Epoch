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

    }
    public void subPage()
    {
        ++pagesFound;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pages"))
        {
            collectUI.text = "Left click to collect";
            if (Input.GetMouseButtonDown(0))
            {
                subPage();
                AkSoundEngine.PostEvent("Acquisition", gameObject);
                pageUI.sprite = pageImages[pagesFound];
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
