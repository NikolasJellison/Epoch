using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Script : MonoBehaviour
{
    public int pagesFound;
    public Image pageUI;
    [Header("0 blocks first, then go up to max")]
    public Sprite[] pageImages;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void subPage()
    {
        ++pagesFound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pages"))
        {
            subPage();
            pageUI.sprite = pageImages[pagesFound];
            print(pageImages[pagesFound].name);
            other.gameObject.SetActive(false);
            
        }
    }
}
