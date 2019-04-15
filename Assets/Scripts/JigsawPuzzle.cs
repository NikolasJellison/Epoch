using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JigsawPuzzle : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    private bool locked;
    private bool moving;
    public JigSawManager jigSawManager;
    private Outline outline;
    private Vector3 offset;

    private void Start()
    {
        originalPosition = transform.position;
        outline = GetComponent<Outline>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (!locked)
        {
            var tempColor = outline.effectColor;
            tempColor.a = 66;
            outline.effectColor = tempColor;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!locked)
        {
            var tempColor = outline.effectColor;
            tempColor.a = 0;
            outline.effectColor = tempColor;
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Shader stuff goes here i suppose

        //Put selected image on top (this is hardcoded index because there are 10 pieces
        if (!locked)
        {
            transform.SetSiblingIndex(9);
            //Offset
            offset = transform.position - Input.mousePosition;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (!locked)
        {
            moving = true;
            transform.position = Input.mousePosition + offset;
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
                //Get rid out outline
                var tempColor = outline.effectColor;
                tempColor.a = 0;
                outline.effectColor = tempColor;
                //Send to back to avoid hiding any pieces
                transform.SetSiblingIndex(0);
            }
            locked = true;
        }
    }
}
