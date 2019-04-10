using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaucetScript : MonoBehaviour
{
    public WaterScript water;
    public Text faucetText;

    // Start is called before the first frame update
    void Start()
    {
        faucetText.text = "";   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!water.startClear)
            {
                // UI
                faucetText.text = "'E' to turn on the water";
                if (Input.GetKey(KeyCode.E))
                {
                    water.startClear = true;
                    water.waterFlow.SetActive(true);
                }
            }
            else
            {
                // UI
                faucetText.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI
        if (other.gameObject.CompareTag("Player"))
        {
            faucetText.text = "";

        }
    }
}
