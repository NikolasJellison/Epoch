using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainScript : MonoBehaviour
{

    public Text moveText;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        moveText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform objBase = transform.GetChild(transform.childCount - 1);
            Vector3 playerObjRay = objBase.position - other.transform.position;
            playerObjRay.y = 0.0f;
            float angle = Vector3.Angle(playerObjRay, other.transform.forward);
            bool facing = angle < 60f;
            //bool facing = true;
            if (!start && !other.gameObject.GetComponent<PlayerController>().lock_movement 
                && !other.gameObject.GetComponent<PlayerController>().manipulating 
                && facing)
            {
                moveText.text = "'E' to move aside";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<Animator>().SetTrigger("Open");
                    start = true;
                }
            }
            else
            {
                moveText.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveText.text = "";
        }
    }
    //*/
}
