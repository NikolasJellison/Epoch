using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToSceneLoad : MonoBehaviour
{
    private SceneLoadManager sceneLoadManager;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoadManager = GameObject.Find("SceneLoadObject").GetComponent<SceneLoadManager>();
        anim = GetComponent<Animator>();
    }

    public void StartFade()
    {
        anim.SetTrigger("StartFade");
    }

    //This function gets called from an event on the fade to black animation
    public void FadeToBlack()
    {
        sceneLoadManager.LoadLivingRoom();
    }
}
