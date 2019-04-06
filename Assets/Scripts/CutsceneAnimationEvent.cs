using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimationEvent : MonoBehaviour
{
    private CutsceneCameraController cameraController;
    private FadeToSceneLoad fadeToSceneLoad;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponentInChildren<CutsceneCameraController>();
        fadeToSceneLoad = GameObject.Find("Image-Fade").GetComponent<FadeToSceneLoad>();
    }

    public void AlertEndOfCry()
    {
        cameraController.canMove = true;
    }

    public void StartFade()
    {
        fadeToSceneLoad.StartFade();
    }
}
