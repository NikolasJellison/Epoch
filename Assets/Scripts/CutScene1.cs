using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene1 : MonoBehaviour
{
    public GameObject player;
    public Camera playerCam;
    public Camera cutSceneCam;
    [Header("Anything that needs to get deleted once the room has dissolved")]
    public GameObject[] delete;
    [Header("These need to have the disolve shader on them")]
    public MeshRenderer[] objectsToDisolve;
    private float dissolveCounter;
    private bool dissolve;

    private void Start()
    {
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        //Debug
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            EnterDesktop();
        }

        //There is a better way but idk how to make the fill continuous
        if (dissolve)
        {
            dissolveCounter += (Time.deltaTime / 3);

            if (dissolveCounter >= 1)
            {
                dissolve = false;
                dissolveCounter = 1;

                foreach(GameObject obj in delete)
                {
                    Destroy(obj);
                }
            }

            foreach (MeshRenderer obj in objectsToDisolve)
            {
                obj.material.SetFloat("_DissolveValue", dissolveCounter);
                //Debug.Log("Dissovlecounter: " + dissolveCounter);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterDesktop();
    }

    public void EnterDesktop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        playerCam.enabled = false;
        cutSceneCam.enabled = true;
    }

    public void ExitDesktop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        dissolve = true;
    }
}
