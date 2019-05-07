using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTypeDetection : MonoBehaviour
{
    //We detect if the player is in WebGl. If so, then we disable the exit buttons 
    private GameObject exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton = GameObject.Find("Exit");
        if (Application.platform == RuntimePlatform.WebGLPlayer && exitButton.GetComponent<Button>() !=null)
        {
            exitButton.SetActive(false);
        }
    }
}
