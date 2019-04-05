using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimationEvent : MonoBehaviour
{
    private CutsceneCameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        cameraController = GetComponentInChildren<CutsceneCameraController>();
    }

    public void AlertEndOfCry()
    {
        cameraController.canMove = true;
    }
}
