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
    private bool jumped;
    private bool sprinted;
    private bool crouched;
    private bool crawled;
    private bool crawledFast;
    private bool openedJournal;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = "Hello, please press <sprite=\"W\" index=\"0\"> <sprite=\"A\" index=\"0\"> <sprite=\"S\" index=\"0\"> <sprite=\"D\" index=\"0\"> to move!";
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
        else if (!sprinted)
        {
            tutorialText.text = "Hold \"LEFT SHIFT\" to sprint";
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                sprinted = true;
            }
        }
        else if (!crouched)
        {
            tutorialText.text = "Press \"C\" to Crouch";
            if (Input.GetKeyDown(KeyCode.C))
            {
                crouched = true;
            }
        }
        else if (!crawled)
        {
            tutorialText.text = "Press <sprite=\"W\" index=\"0\"> <sprite=\"A\" index=\"0\"> <sprite=\"S\" index=\"0\"> <sprite=\"D\" index=\"0\"> while crouching to crawl";
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
            {
                crawled = true;
            }
        }
        else if (!crawledFast)
        {
            tutorialText.text = "Hold \"LEFT SHIFT\" while crouching to crawl faster";
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                crawledFast = true;
            }
        }
        //Getting rid of the jorunal until we add it into the player controller
        
        else if (!openedJournal)
        {
            tutorialText.text = "Press <sprite=\"Tab\" index=\"0\"> to open journal";
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                openedJournal = true;
            }
        }
        
        else
        {
            tutorialText.text = "You've completed this tutorial";
            Destroy(gameObject, 6);
        }
    }
}
