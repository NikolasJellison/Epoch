using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToHallwayLevel : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            sceneLoadManager.LoadHallWay();
            Debug.Log("Sending player to level 2");
        }
    }
}
