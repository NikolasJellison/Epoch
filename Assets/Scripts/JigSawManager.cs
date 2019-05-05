using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JigSawManager : MonoBehaviour
{
    private int counter;
    public GameObject exitButton;
    //public Image completedVersion;
    public GameObject panel;
    public TextMeshProUGUI text;
    public Color[] textColors = new Color[4];

    private void Start()
    {
        exitButton.SetActive(false);
        
    }

    public void Update()
    {
        float index = DataScript.colorblindMode;
        text.color = textColors[(int)index];
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
