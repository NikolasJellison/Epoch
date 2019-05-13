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
            Transform objBase = transform;
            Vector3 playerObjRay = objBase.position - other.transform.position;
            playerObjRay.y = 0.0f;
            float angle = Vector3.Angle(playerObjRay, other.transform.forward);
            bool facing = angle < 45f;

            if (!water.startClear && !other.gameObject.GetComponent<PlayerController>().lock_movement &&
                !other.gameObject.GetComponent<PlayerController>().manipulating &&
                facing)
            {
                // UI
                faucetText.text = "'E' to turn on the water";
                if (Input.GetKey(KeyCode.E))
                {
                    water.startClear = true;
                    print("BEGIN: " + Time.fixedTime);
                    water.waterFlow.SetActive(true);
                    faucetText.text = "";
                    GetComponent<AudioSource>().Play(); 
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
