using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneLastAnimationEvent : MonoBehaviour
{
    public CutsceneCameraController cutscene;
    public void EndDis()
    {
        cutscene.canMove = true;
        cutscene.screenOn = true;
    }
}
