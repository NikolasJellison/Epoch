using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    public GUISkin skin;
    public Color highlightColor = Color.cyan;
    private Color originalColor;

    private void OnGUI()
    {
        GUI.skin = skin;
    }

    private void OnMouseEnter()
    {
        originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = highlightColor;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
}
