using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColormodeScript : MonoBehaviour
{
    public List<MeshRenderer> sceneMRs;
    public List<SkinnedMeshRenderer> sceneSMRs;
    public List<Material> sceneMats;
    public bool dirty;
    public float currentMode;
    // Start is called before the first frame update
    
    void Start()
    {
        currentMode = 0;
        dirty = false;
        sceneMRs = new List<MeshRenderer>(GameObject.FindObjectsOfType<MeshRenderer>());
        sceneSMRs = new List<SkinnedMeshRenderer>(GameObject.FindObjectsOfType<SkinnedMeshRenderer>());
        foreach (SkinnedMeshRenderer rend in sceneSMRs)
        {
            foreach (Material mat in rend.materials)
            {
                if (!sceneMats.Contains(mat))
                {
                    if (!sceneMats.Contains(mat))
                    {

                        if (mat.shader.name == "Shader Graphs/ColorBlindTest")
                        {
                            sceneMats.Add(mat);
                        }
                        /*
                        else
                        {
                            string message = rend.gameObject.name + ": " + mat.name + ", " + mat.shader.name;
                            if (rend.transform.parent != null)
                            {
                                message = rend.transform.parent.name + "/" + message;
                            }
                            print(message);
                        }
                        //*/
                    }
                }
            }

        }

        foreach (MeshRenderer rend in sceneMRs)
        {
            foreach(Material mat in rend.materials)
            {
                if (!sceneMats.Contains(mat))
                {
                    
                    if(mat.shader.name == "Shader Graphs/ColorBlindTest")
                    {
                        sceneMats.Add(mat);
                    }
                    /*
                    else
                    {
                        string message = rend.gameObject.name + ": " + mat.name +", " + mat.shader.name;
                        if(rend.transform.parent != null)
                        {
                            message = rend.transform.parent.name + "/" + message;
                        }
                        print(message);
                        

                    }
                    //*/
                    //print();
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = 0.0f;
            dirty = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = 1.0f;
            dirty = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentMode = 2.0f;
            dirty = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentMode = 3.0f;
            dirty = true;
        }
        if (dirty)
        {
            foreach (Material mat in sceneMats)
            {
                mat.SetFloat("_colorblind", currentMode);
            }
            dirty = false;
        }
    }
}
