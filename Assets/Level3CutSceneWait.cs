using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3CutSceneWait : MonoBehaviour
{
    public PlayerController playerC;

    private void OnEnable()
    {
        playerC.lock_movement = true;
        playerC.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        playerC.lock_movement = false;
        playerC.gameObject.SetActive(true);
    }
}
