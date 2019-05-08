using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleScript : MonoBehaviour
{
    public GameObject player;
    public GameObject playerUI;
    public PerspectiveSwap vantageMgr;
    public AudioSource radio;
    //public OldCameraController cam;
    private bool started;
    private bool appeared;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(started && radio.volume > 0)
        {
            float volume = radio.volume;
            volume -= .8f * Time.deltaTime;
            radio.volume = Mathf.Max(0.0f, volume);
        }
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
                player.GetComponent<PlayerController>().journalInput = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerUI.SetActive(false);
                //AkSoundEngine.PostEvent("StopAdultMusic", radio);
                Instantiate(Resources.Load("Canvas-Shuffle"));
            }
            
        }
        
    }
}
