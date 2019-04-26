using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighLight : MonoBehaviour
{
    public GUISkin skin;
    [ColorUsage(true,true)]
    public Color highlightColor = Color.cyan;
    private Color originalColor;
    //Lists to store originalColors
    private List<Material> storeMats = new List<Material>();
    private List<Color> storeColors = new List<Color>();
    //For raycasts
    RaycastHit hit;
    RaycastHit storeHit;

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

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit) && hit.transform.root.GetComponent<HighLightInfo>() != null)
            {
                //Debug.Log(hit.collider.name);
                if (storeMats.Count > 0 && hit.transform.root != storeHit.collider.transform.root)
                {
                    storeHit.transform.root.GetComponent<HighLightInfo>().showFlavorText = false;
                    Debug.Log("UnGLow");
                    UnGlow();
                }
                else if (storeMats.Count > 0)
                {
                    hit.transform.root.GetComponent<HighLightInfo>().showFlavorText = true;
                    return;
                }
                storeHit = hit;
                Glow(hit);
            }
            else
            {
                storeHit.transform.root.GetComponent<HighLightInfo>().showFlavorText = false;
                UnGlow();
            }
        }
    }

    private void Glow(RaycastHit hit)
    {
        foreach (Renderer r in hit.transform.root.GetComponentsInChildren<Renderer>())
        {
            //Store list
            storeMats.Add(r.material);
            storeColors.Add(r.material.color);
            originalColor = r.material.color;
            //material.color is just setting the varible in the shader called "_Color". so if a shader doesn't have a base color by that name, this won't work
            r.material.color = highlightColor;
        }
    }

    private void UnGlow()
    {
        for(int i = 0; i < storeMats.Count; i++)
        {
            storeMats[i].color = storeColors[i];
        }

        storeMats.Clear();
        storeColors.Clear();
    }
}
