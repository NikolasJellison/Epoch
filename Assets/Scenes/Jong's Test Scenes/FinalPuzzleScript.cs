using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerUI;
    public PerspectiveSwap vantageMgr;
    //public OldCameraController cam;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!started)
            {
                
                started = true;
                //cam.enabled = false;
                vantageMgr.enabled = false;
                player.GetComponent<PlayerController>().lock_movement = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerUI.SetActive(false);
                //AkSoundEngine.PostEvent("StopAdultMusic", radio);
                Instantiate(Resources.Load("Canvas-Shuffle"));
            }
            
        }
        
    }
}
