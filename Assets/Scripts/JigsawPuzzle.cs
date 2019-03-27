using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JigsawPuzzle : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalPosition;
    private bool locked;
    private bool moving;
    public JigSawManager jigSawManager;

    private void Start()
    {
        originalPosition = transform.position;
    }

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
        if(transform.position.x < 0 || transform.position.x > Screen.width)
        {
            transform.position = originalPosition;
        }
        else if(transform.position.y < 0 || transform.position.y > Screen.height)
        {
            transform.position = originalPosition;
        }

        moving = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.name == gameObject.name + " Location" && !moving)
        {
            transform.position = collision.transform.position;
            if (!locked)
            {
                jigSawManager.CorrectPlacement();
            }
            locked = true;
        }
    }
}
