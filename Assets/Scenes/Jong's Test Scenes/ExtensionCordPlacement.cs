using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtensionCordPlacement : MonoBehaviour
{
    public VisionObjectScript fanVO;
    public PlayerController controller;
    public Transform holderBase;
    public Transform objBase;
    public GameObject extensionShelf;
    public GameObject extensionPlaced;
    public float distanceThresh = 2f;
    public Text UI;
    public Text actUI;
    public GameObject spinner;
    public NoteScriptL3 note;
    public bool done;

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
        if (done)
        {
            UI.text = "";
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Transform triggerBase = transform.GetChild(transform.childCount - 1);
            Vector3 playerObjRay = triggerBase.position - other.transform.position;
            playerObjRay.y = 0.0f;
            float angle = Vector3.Angle(playerObjRay, other.transform.forward);
            bool facing = angle < 60f;

            if (fanVO.enabled || controller.manipulating || controller.lock_movement || !facing)
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
                    holderBase.transform.parent.gameObject.tag = "Untagged";
                    if (other.GetComponent<PlayerController>().moveableCandidates.Contains(holderBase.transform.parent.gameObject))
                    {
                        other.GetComponent<PlayerController>().moveableCandidates.Remove(holderBase.transform.parent.gameObject);
                    }
                    GetComponent<AudioSource>().Play();
                    UI.text = "";
                    actUI.text = "";
                    extensionPlaced.SetActive(true);
                    spinner.SetActive(true);
                    note.enabled = true;
                    done = true;
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
