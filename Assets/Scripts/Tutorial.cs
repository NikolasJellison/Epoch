using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    //public Text tutorialText;
    public TextMeshProUGUI tutorialText;
    //Tracking steps in tutorial?
    private bool walked;
    private bool sprinted;
    private bool jumped;
    private bool crouched;
    private bool openedJournal;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "Hello, please press <sprite=\"WASD01\" index=\"0\"> to move!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!walked)
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
            {
                walked = true;
            }
        }
        else if (!jumped)
        {
            tutorialText.text = "Press <sprite=\"Space01\" index=\"0\"> to jump";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;
            }

        }
        else if (!crouched)
        {
            tutorialText.text = "Press <sprite=\"EKey01\" index=\"0\"> to crouch?";
            if (Input.GetKeyDown(KeyCode.E))
            {
                crouched = true;
            }
        }
        else if (!openedJournal)
        {
            tutorialText.text = "Press <sprite=\"TabOREscape01\" index=\"0\"> to open journal";
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                openedJournal = true;
            }
        }
        else
        {
            tutorialText.text = "You've completed this tutorial";
            Destroy(gameObject, 3);
        }
    }
}
