using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class WorldSpaceUIDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Text names")]
    public string defaultName;
    public string hoverName;

    [Space(10)]
    [Header("Is does object has toggle state? If so, need to fill out below")]
    [Tooltip("Ex: Locker, where you need to open and close ")] public bool twoStates;
    [Tooltip("This is only used if 'twoStates' is true")]public string hoverNameAfterClick;
    [Tooltip("The camera that gets swapped to after a click")]public Camera newViewCamera;
    [Space(10)]
    
    private Transform player;
    private TextMeshProUGUI selfText;
    [Header("Images")]
    public Image defaultImage;
    public Image hoverImage;
    //need to set the canvas' event camera correctly
    private Camera cam;


    private void Start()
    {
        //change this to player in actual build
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        selfText = GetComponentInChildren<TextMeshProUGUI>();
        hoverImage.enabled = false;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
    }
    private void RayCastWorld()
    {
        //On left click right now
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        selfText.text = hoverName;
        selfText.faceColor = Color.blue;
        defaultImage.enabled = false;
        hoverImage.enabled = true;
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        selfText.text = defaultName;
        defaultImage.enabled = true;
        hoverImage.enabled = false;
        selfText.faceColor = Color.red;
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        selfText.faceColor = Color.green;
        Debug.Log("Cursor Clicking on  " + name + " GameObject");
    }
}
