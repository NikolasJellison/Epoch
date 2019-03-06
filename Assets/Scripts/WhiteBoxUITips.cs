using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBoxUITips : MonoBehaviour
{
    RaycastHit hit;
    public string purple;
    public string blue;
    public string yellow;
    public string red;
    public Text whiteBoxUIText;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward),Color.blue,Mathf.Infinity);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            switch (hit.transform.tag)
            {
                case "WhiteBox-Yellow":
                    whiteBoxUIText.text = yellow;
                    break;

                case "WhiteBox-Blue":
                    whiteBoxUIText.text = blue;
                    break;

                case "WhiteBox-Purple":
                    whiteBoxUIText.text = purple;
                    break;

                case "WhiteBox-Red":
                    whiteBoxUIText.text = red;
                    break;

                default:
                    whiteBoxUIText.text = "";
                    break;
            }
        }
        
    }
}
