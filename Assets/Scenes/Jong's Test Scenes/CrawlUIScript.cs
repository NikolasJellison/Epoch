using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrawlUIScript : MonoBehaviour
{
    public Text crawlUI;
    public PerspectiveSwap vantageMgr;
    // Start is called before the first frame update
    void Start()
    {
        crawlUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crawlUI.text = "'E' to crawl";
        }
        if (!vantageMgr.playerActive)
        {
            crawlUI.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crawlUI.text = "'E' to crawl";
        }
        if (!vantageMgr.playerActive)
        {
            crawlUI.text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            crawlUI.text = "";
        }
    }
}
