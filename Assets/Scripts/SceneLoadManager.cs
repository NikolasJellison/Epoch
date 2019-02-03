using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    /*This is assuming the build order for the scenes is 
     * [0] Title
     * [1] LivingRoom
     * [2] Hallway
     */

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLivingRoom()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHallWay()
    {
        SceneManager.LoadScene(2);
    }


}
