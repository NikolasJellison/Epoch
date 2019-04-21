using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public Text collectUI;
    // Start is called before the first frame update
    void Start()
    {
        collectUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectUI.text = "Left Click to pick up";
            if (Input.GetMouseButtonDown(0))
            {
                collectUI.text = "";
                gameObject.SetActive(false);
            }
        }
    }
    //*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectUI.text = "";
        }
    }
}
