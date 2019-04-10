using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleScript : MonoBehaviour
{
    public GameObject radio;
    public GameObject player;
    public GameObject playerUI;
    public PerspectiveSwap vantageMgr;
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
            vantageMgr.cutSceneActive = true;
            player.GetComponent<PlayerController>().lock_movement = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerUI.SetActive(false);
            AkSoundEngine.PostEvent("StopAdultMusic", radio);
            Instantiate(Resources.Load("Canvas-Shuffle"));
        }
        
    }
}
