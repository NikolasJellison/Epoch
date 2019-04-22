using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShuffleMovement : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    private bool locked;
    private bool moving;
    private Vector3 offset;
    private Color outlineColor;
    private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        //Get Original Outline color
        outline = GetComponent<Outline>();
        outlineColor = outline.effectColor;
    }

    //Copy paste of code from JigsawPuzzle.cs
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Shader stuff goes here i suppose
        offset = transform.position - Input.mousePosition;
        //Debug.Log("MOuse pos: " + Input.mousePosition);
        //Debug.Log("Transform pos: " + transform.position);
        //Debug.Log("Offset: " + offset);

        //Set to top
        transform.SetAsLastSibling();
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

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!locked)
        {
            var tempColor = outline.effectColor;
            tempColor.a = 0;
            outline.effectColor = tempColor;
        }
    }
}
