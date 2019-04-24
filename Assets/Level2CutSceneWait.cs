using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2CutSceneWait : MonoBehaviour
{
    public PlayerController playerC;
    public GameObject teenModelCutscene;
    private Transform t;
    public GameObject pointLight3;
    private void OnEnable()
    {
        playerC.lock_movement = true;
        playerC.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        playerC.lock_movement = false;
        playerC.gameObject.SetActive(true);
        playerC.gameObject.transform.position = teenModelCutscene.transform.position;
        //hard code because this is taking too long
        Debug.Log(playerC.gameObject.transform.eulerAngles);
        playerC.gameObject.transform.eulerAngles =  new Vector3(0f, 90f, 0f);
        Debug.Log(playerC.gameObject.transform.eulerAngles);
        pointLight3.SetActive(true);
    }
}
