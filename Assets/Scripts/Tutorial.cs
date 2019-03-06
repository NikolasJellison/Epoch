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

    private bool activatedObject;
    public GameObject cutSceneCamera;
    public GameObject playerCamera;
    public GameObject visionObject;
    private float cutsceneTime;
    public Transform center;
    private Transform originalPlayer;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneTime = 0.0f;
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
        //*/
        else if(!activatedObject)
        {
            tutorialText.text = "Some objects are intangible. To activate them, align your crosshair with the corresponding symbol";

            // cutscene showing a camera moving around the chair
            if(cutsceneTime < 3.76f)
            {
                playerCamera.SetActive(false);
                cutSceneCamera.SetActive(true);
                if (originalPlayer == null)
                {
                    originalPlayer = visionObject.GetComponent<PerspectiveScript>().player;
                }
                visionObject.GetComponent<PerspectiveScript>().player = cutSceneCamera.transform;
                
                cutSceneCamera.transform.forward = center.position - cutSceneCamera.transform.position;
                cutSceneCamera.transform.RotateAround(center.position, Vector3.up, 50 * Time.deltaTime);
                cutsceneTime += Time.deltaTime;
            }
            else
            {
                tutorialText.text = "Some objects are intangible. To activate them, align your crosshair with the corresponding symbol (Press <sprite=\"Space01\" index=\"0\"> to continue)";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    visionObject.GetComponent<PerspectiveScript>().player = originalPlayer;
                    playerCamera.SetActive(true);
                    cutSceneCamera.SetActive(false);
                    activatedObject = true;
                }
            }
            // make player invisible(ish) & disable the player controller
        }
        //Getting rid of the jorunal until we add it into the player controller
        else if (!openedJournal)
        {
            tutorialText.text = "Press <sprite=\"Tab\" index=\"0\"> to open journal";
            if (Input.GetKeyDown(KeyCode.Tab))
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
