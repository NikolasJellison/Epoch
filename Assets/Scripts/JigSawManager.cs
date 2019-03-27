using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigSawManager : MonoBehaviour
{
    private int counter;
    public GameObject exitButton;
    //public Image completedVersion;
    public GameObject panel;

    private void Start()
    {
        exitButton.SetActive(false);
    }


    public void CorrectPlacement()
    {
        counter++;
        //hard code for sprint
        if(counter == 9)
        {
            EndGame();
        }
    }

    public void Reset()
    {
        Debug.Log("Reloading");
        Instantiate(Resources.Load("Canvas-Jigsaw"));
        Destroy(gameObject);
    }

    public void EndGame()
    {
        exitButton.SetActive(true);
        //panel.SetActive(false);
        //completedVersion.enabled = true;
    }

    public void ExitGame()
    {
        Destroy(gameObject);
    }
}
