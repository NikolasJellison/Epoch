using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CutSceneLast : MonoBehaviour
{
    //Some of this stuff is just copied from CutScene1
    public GameObject player;
    private CutsceneCameraController cameraController;
    public Camera playerCam;
    public Camera cutSceneCam;
    public AudioSource radio;
    public AudioSource ambiance;
    public GameObject computerScreenCanvas;
    public GameObject cutsceneUI;
    private Quaternion targetOGRotation;
    private bool startFade;
    public Image fadeImage;
    bool cursorOn;
    bool decreaseAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        cameraController = player.GetComponentInChildren<CutsceneCameraController>();
        //cameraController.canMove = true;
    }

    private void Update()
    {
        if (decreaseAudio && ambiance.volume > 0)
        {
            float volume = ambiance.volume;
            volume -= 0.3f * Time.deltaTime;
            ambiance.volume = Mathf.Max(0.0f, volume);
        }
        if (cursorOn)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EnterDesktop()
    {
        cursorOn = true;
        player.GetComponent<PlayerController>().enabled = false;
        playerCam.enabled = false;
        cutSceneCam.enabled = true;
    }

    public void EndGame()
    {
        //ambiance.Stop();
        decreaseAudio = true;
        radio.Play();
        cutsceneUI.SetActive(true);
        cutsceneUI.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        StartCoroutine(WaitABit());
    }

    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(5);
        startFade = true;
        fadeImage.GetComponent<FadeToSceneLoad>().StartFade();
    }
}
