using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightInfo : MonoBehaviour
{
    public GUISkin skin;
    [TextArea]
    public string flavorText = "This object means nothing to me.";
    public Vector2 boxSize = new Vector2(150, 25);
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
            GUI.Box(new Rect(Event.current.mousePosition.x - (boxSize[0] + 5), Event.current.mousePosition.y, boxSize[0],boxSize[1]), flavorText);
        }
    }
}
