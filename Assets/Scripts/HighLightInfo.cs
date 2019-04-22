using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightInfo : MonoBehaviour
{
    public GUISkin skin;
    [TextArea]
    public string flavorText = "This object means nothing to me.";
    [HideInInspector]public bool showFlavorText;

    private void OnGUI()
    {
        GUI.skin = skin;
        ShowText();
    }

    public void ShowText()
    {
        if (showFlavorText)
        {
            GUI.Box(new Rect(Event.current.mousePosition.x - 155, Event.current.mousePosition.y, 150, 25), flavorText);
        }
    }
}
