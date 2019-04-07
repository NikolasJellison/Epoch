using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimationEvent : MonoBehaviour
{
    private CutsceneCameraController cameraController;
    private FadeToSceneLoad fadeToSceneLoad;
    private CutScene1 cutScene;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponentInChildren<CutsceneCameraController>();
        fadeToSceneLoad = GameObject.Find("Image-Fade").GetComponent<FadeToSceneLoad>();
        cutScene = GameObject.Find("CutScene Manager").GetComponent<CutScene1>();
    }

    public void AlertEndOfCry()
    {
        cameraController.canMove = true;
        cutScene.dissolve = true;
    }

    public void StartFade()
    {
        fadeToSceneLoad.StartFade();
    }
}
