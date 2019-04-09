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
    public GameObject computerScreenCanvas;
    public GameObject cutsceneUI;
    private Quaternion targetOGRotation;
    private bool startFade;
    public Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        playerCam.enabled = true;
        cutSceneCam.enabled = false;
        cameraController = player.GetComponentInChildren<CutsceneCameraController>();
        //cameraController.canMove = true;
    }

    public void EnterDesktop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<PlayerController>().enabled = false;
        playerCam.enabled = false;
        cutSceneCam.enabled = true;
    }

    public void EndGame()
    {
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
