using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionCordPlacement : MonoBehaviour
{
    public VisionObjectScript fanVO;
    public Transform holderBase;
    public Transform objBase;
    public GameObject extensionShelf;
    public GameObject extensionPlaced;
    public float distanceThresh = 2f;
    public Text UI;
    public GameObject spinner;
    public NoteScriptL3 note;

    // Start is called before the first frame update
    void Start()
    {
        UI.text = "";
        extensionPlaced.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (fanVO.enabled)
            {
                UI.text = "";
            }
            else if (extensionShelf.activeSelf)
            {
                UI.text = "I need something to plug the fan in";
            }
            else if (Vector3.Distance(holderBase.position, objBase.position) > distanceThresh)
            {
                print(Vector3.Distance(holderBase.position, objBase.position));
                UI.text = "I need something to rest the cord on";
            } else
            {
                UI.text = "Left Click to plug in the fan";
                if (Input.GetMouseButtonDown(0))
                {
                    UI.text = "";
                    extensionPlaced.SetActive(true);
                    spinner.SetActive(true);
                    note.enabled = true;
                }
            }
            //UI.text = "Left Click to pick up";
            /*
            
            //*/
        }
    }
    //*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UI.text = "";
        }
    }
}
