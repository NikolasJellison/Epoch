using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeColormodeScript : MonoBehaviour
{
    public List<MeshRenderer> sceneMRs;
    public List<SkinnedMeshRenderer> sceneSMRs;
    public List<Material> sceneMats;
    
    public Color[] particleColors;
    public List<MeshRenderer> objectives = new List<MeshRenderer>();
    //private List<Material> objectiveMats = new List<Material>();
    // Color: 0 = Reguar, 1 = Deuteranopia, 2 = Protanopia, 3 = Tritanopia
    public Color[] objectiveColors = new Color[4];
    public Material visionMat;
    public Color[] visionColors = new Color[4];
    // Cutscenes
    public GameObject chair;
    // level 1
    public TextMeshProUGUI tutorialText;
    public GameObject[] particles;
    // Level 2
    public List<Material> hallwayLights = new List<Material>();
    public Color[] hallwayLightColors = new Color[4];
    public List<Light> lights = new List<Light>();
    //public Color[] lightColors = new Color[4];
    public List<Text> text;
    public Color[] textColors = new Color[4];
    public Light objectiveLight;
    // Level 3
    public List<MeshRenderer> waterMats;
    public Color[] waterColors = new Color[4];
    public Material stopper;

    // 
    public bool dirty;
    public float currentMode;

    //Other shaders
    [Header("Shader Names (exclude ShaderGraphs part)")]
    [SerializeField]
    public string[] shaderNames = new string[] 
    {"Breakable_new","Cutscene1Fall","Dissolve Test1_new","GlowTest_new",
        "Hallway Walls_New","Lamp_New","ToyBox","TransparentBlocks","Void"
    };
    public List<Renderer> shaderRend = new List<Renderer>();
    public List<Material> shaderMaterials = new List<Material>();

    
    void Start()
    {
        //text[0].col
        //currentMode = 0;
        currentMode = DataScript.colorblindMode;
        dirty = false;
        sceneMRs = new List<MeshRenderer>(GameObject.FindObjectsOfType<MeshRenderer>());
        sceneSMRs = new List<SkinnedMeshRenderer>(GameObject.FindObjectsOfType<SkinnedMeshRenderer>());
        text = new List<Text>(GameObject.FindObjectsOfType<Text>());

        //Other shaders --
        shaderRend = new List<Renderer>(GameObject.FindObjectsOfType<Renderer>());
        foreach(Renderer r in shaderRend)
        {
            foreach(string s in shaderNames)
            {
                //Some gameobjects have multiple materials on 1 renderer, so we check those
                foreach(Material m in r.materials)
                {
                    if (m.shader.name == "Shader Graphs/" + s)
                    {
                        //Debug.Log("Added: '" + r.gameObject.name + "' to shaderMaterials list because its shader was: " + s + ". Its material is: " + m.name);
                        shaderMaterials.Add(m);
                    }
                }
            }
        }
        // -- Other Shaders

        foreach (SkinnedMeshRenderer rend in sceneSMRs)
        {
            foreach (Material mat in rend.materials)
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

        foreach (MeshRenderer rend in sceneMRs)
        {
            foreach(Material mat in rend.materials)
            {
                if (mat == null)
                {
                    continue;
                }
                /*
                if (mat.shader.name == "Shader Graphs/Objectives")
                {
                    objectiveMats.Add(mat);
                }
                //*/
                if (mat.name == "Hallway Light (Instance)" || mat.name == "Hallway Light")
                {
                    hallwayLights.Add(mat);
                }

                if (!sceneMats.Contains(mat))
                {   
                    if (mat.shader.name == "Shader Graphs/ColorBlindTest")
                    {
                        sceneMats.Add(mat);
                    }
                    else
                    {
                        string message = rend.gameObject.name + ": " + mat.name +", " + mat.shader.name;
                        if(rend.transform.parent != null)
                        {
                            message = rend.transform.parent.name + "/" + message;
                        }
                        //Sprint(message);
                        

                    }
                    //*/
                    //print();
                }
            }
            
        }
        ChangeMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentMode = 0.0f;
            DataScript.colorblindMode = currentMode;
            dirty = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentMode = 1.0f;
            DataScript.colorblindMode = currentMode;
            dirty = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentMode = 2.0f;
            DataScript.colorblindMode = currentMode;
            dirty = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentMode = 3.0f;
            DataScript.colorblindMode = currentMode;
            dirty = true;
        }

        if (dirty)
        {
            ChangeMode();
            dirty = false;
        }


    }

    private void ChangeMode()
    {
        //Other shaders --
        foreach (Material m in shaderMaterials)
        {
            if(m == null)
            {
                continue;
            }
            m.SetFloat("_colorblind", currentMode);
        }
        // -- Other shaders
        foreach (Material mat in sceneMats)
        {
            if (mat == null)
            {
                continue;
            }
            mat.SetFloat("_colorblind", currentMode);
        }
        int index = (int)currentMode;
        visionMat.SetColor("_color", visionColors[index]);

        foreach(MeshRenderer objMesh in objectives)
        {
            foreach(Material mat in objMesh.materials)
            {
                if(mat == null)
                {
                    continue;
                }
                //sprint(mat.name);
                if (mat.name.Contains("Objectives"))
                {
                    mat.SetColor("_Glow", objectiveColors[index]);
                }
            }
        }
        /*
        foreach (Material mat in objectiveMats)
        {
            mat.SetColor("_Glow", objectiveColors[index]);
        }
        //*/

        foreach (Text t in text)
        {
            t.color = textColors[index];
        }
        if (tutorialText != null)
        {
            tutorialText.color = textColors[index];
        }

        if (particles.Length > 0)
        {
            foreach (GameObject pSystem in particles)
            {
                pSystem.SetActive(false);
            }
            particles[index].SetActive(true);
        }
        
        if (hallwayLights.Count > 0)
        {
            foreach (Material light in hallwayLights)
            {
                if (light != null)
                {
                    light.SetColor("_EmissionColor", hallwayLightColors[index]);
                }

            }
        }

        if (lights.Count > 0)
        {
            foreach (Light light in lights)
            {
                if (light != null)
                {
                    light.color = hallwayLightColors[index];
                }
            }
        }

        if (objectiveLight != null)
        {
            objectiveLight.color = objectiveColors[index];
        }

        if (stopper != null)
        {
            stopper.SetColor("_color", visionColors[index]);
        }

        if(chair != null)
        {
            foreach (MeshRenderer mesh in chair.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.SetColor("_color", visionColors[index]);
            }
        }
        
        
    }
}
