﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillswitchScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}