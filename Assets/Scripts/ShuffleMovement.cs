﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShuffleMovement : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    private Vector3 originalPosition;
    private bool locked;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    //Copy paste of code from JigsawPuzzle.cs
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Shader stuff goes here i suppose
    }

    public void OnDrag(PointerEventData data)
    {
        if (!locked)
        {
            moving = true;
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Incase the puzzle piece is off the screen and left there
        if (transform.position.x < 0 || transform.position.x > Screen.width)
        {
            transform.position = originalPosition;
        }
        else if (transform.position.y < 0 || transform.position.y > Screen.height)
        {
            transform.position = originalPosition;
        }

        moving = false;
    }
}