﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetScript : MonoBehaviour
{
    public Text cabinetUI;
    public GameObject key;
    public BoxCollider note;
    public GameObject glow;
    public GameObject[] pivots;

    // Start is called before the first frame update
    void Start()
    {
        cabinetUI.text = "";
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

            if (!pivots[0].activeSelf && !other.gameObject.GetComponent<PlayerController>().lock_movement &&
                !other.gameObject.GetComponent<PlayerController>().manipulating &&
                facing)
            {
                if (key != null)
                {
                    if (!key.activeSelf)
                    {
                        cabinetUI.text = "'E' to Open";
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            glow.SetActive(false);
                            note.enabled = true;
                            foreach(GameObject pivot in pivots)
                            {
                                pivot.SetActive(true);
                            }
                        }
                    }
                    else
                    {
                        cabinetUI.text = "It's locked";
                    }
                }
                
            }
            else
            {
                cabinetUI.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cabinetUI.text = "";
        }
    }
}
