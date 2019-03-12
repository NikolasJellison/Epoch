using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneLoadManager : MonoBehaviour
{
    public UnityEvent goNextLevel;

    /*This is assuming the build order for the scenes is 
     * [0] Title
     * [1] LivingRoom
     * [2] Hallway
     * [3] Apartment
     * [4] End Screen
     * [5] Cutscene 1
     */

    private void OnTriggerEnter(Collider other)
    {
        if(goNextLevel != null && other.tag == "Player")
        {
            goNextLevel.Invoke();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCutScene1()
    {
        SceneManager.LoadScene("Cutscene 1");
    }

    public void LoadLivingRoom()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHallWay()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadApartment()
    {
        SceneManager.LoadScene(3);
    }
    public void loadEndScreen()
    {
        SceneManager.LoadScene(4);
    }


}
