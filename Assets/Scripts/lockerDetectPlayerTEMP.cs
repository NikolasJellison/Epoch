using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class lockerDetectPlayerTEMP : MonoBehaviour
{
    //Delete this right after turn in, this code is only meant to be used on 1 locker
    //Most of this stuff is taken from "WorldSpaceUIDetect.cs" just trying to make a usuable interaction
    [Header("Detection Range")]
    public float range = 8;
    private bool inRange;
    private Canvas canvas;
    [Header("Text names")]
    public string defaultName;
    public string hoverName;

    [Space(10)]
    [Header("Is does object has toggle state? If so, need to fill out below")]
    [Tooltip("Ex: Locker, where you need to open and close ")] public bool twoStates;
    [Tooltip("This is only used if 'twoStates' is true")] public string hoverNameAfterClick;
    [Tooltip("The camera that gets swapped to after a click")] public Camera newViewCamera;
    [Space(10)]

    private Transform player;
    private TextMeshProUGUI selfText;
    [Header("Images")]
    public Image defaultImage;
    public Image hoverImage;
    //need to set the canvas' event camera correctly
    private Camera cam;
    private bool lockerOpen;
    public Locker locker;


    private void Start()
    {
        //change this to player in actual build
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        selfText = GetComponentInChildren<TextMeshProUGUI>();
        hoverImage.enabled = false;
        canvas = GetComponent<Canvas>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            inRange = true;
            canvas.enabled = true;
        }
        else
        {
            inRange = false;
            canvas.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && inRange)
        {
            locker.OpenLocker();
            lockerOpen = !lockerOpen;
        }

        if (lockerOpen)
        {
            selfText.text = hoverNameAfterClick;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            selfText.text = hoverName;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
