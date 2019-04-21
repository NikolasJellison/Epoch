using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCutscene : MonoBehaviour
{
    public GameObject goal;
    private Vector3 screenPoint;
    private Vector3 offset;
    public float jumpLengthMultiplier = .2f;
    private float zStore;
    //Not to be confused with jumping
    //True at first so the blocks don't go moving in the toy box
    public bool isMoving = true;
    private bool locked;
    private float yStore;
    [HideInInspector] public bool inGoal;
    private bool inPlace;
    private float timeA;
    private ToyBox toyBox;


    private void Start()
    {
        //Get ToyBox script
        toyBox = GameObject.Find("ToyBox").GetComponent<ToyBox>();
        zStore = transform.position.z;
        yStore = transform.position.y;
        //goal.SetActive(false);
    }

    private void OnEnable()
    {
        //So the blocks start moving at the beginning
        isMoving = false;
    }

    private void Update()
    {
        timeA += Time.deltaTime;
        //"Jumping" blocks
        if (!isMoving && !inPlace)
        {
            transform.position = new Vector3(transform.position.x, yStore + (Mathf.Sin(timeA) * jumpLengthMultiplier), transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isMoving = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        isMoving = true;
        Vector3 mouseScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPoint) + offset;
        transform.position = new Vector3 (mousePos.x, mousePos.y, zStore);
    }

    private void OnMouseUp()
    {
        isMoving = false;
        yStore = transform.position.y;
        timeA = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Goal")
        {
            isMoving = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isMoving && other.tag == "Goal")
        {
           // if (!other.GetComponent<BlockCutsceneTransparentHolders>().occupied)
           // {
                if (other.name == goal.name)
                {
                    inGoal = true;
                    other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
                else
                {
                    inGoal = false;
                    other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
                inPlace = true;
                transform.position = other.transform.position;
                //other.GetComponent<BlockCutsceneTransparentHolders>().occupied = true;
            //} 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Goal")
        {
            other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            inPlace = false;
           // other.GetComponent<BlockCutsceneTransparentHolders>().occupied = false;
        }
    }

    public void CutSceneBegins()
    {
        goal.SetActive(true);
    }

}
