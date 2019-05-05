using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeColormodeScript : MonoBehaviour
{
    public List<MeshRenderer> sceneMRs;
    public List<SkinnedMeshRenderer> sceneSMRs;
    public List<Material> sceneMats = new List<Material>();
    public List<MeshRenderer> objectives = new List<MeshRenderer>();
    // Color: 0 = Reguar, 1 = Deuteranopia, 2 = Protanopia, 3 = Tritanopia
    public Color[] objectiveColors = new Color[4];
    public Material visionMat;
    public Color[] visionColors = new Color[4];
    // Cutscenes
    public GameObject chair;
    [Header("Level 1")]
    // level 1
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI blockPuzzleText;
    public List<GameObject> particles = new List<GameObject>();
    public Light ominousLight;
    public Color[] omLightColors = new Color[4];
    public List<MeshRenderer> lamps = new List<MeshRenderer>();
    public Color[] lampColors = new Color[4];
    public List<MeshRenderer> toyboxParts = new List<MeshRenderer>();
    [Header("Level 2")]
    // Level 2
    public List<Material> hallwayLights = new List<Material>();
    public Color[] hallwayLightColors = new Color[4];
    public List<Light> lights = new List<Light>();
    public List<Text> text;
    public Color[] textColors = new Color[4];
    public Light objectiveLight;
    public List<MeshRenderer> posters = new List<MeshRenderer>();
    public Color[] posterColors = new Color[4];
    [Header("Level 3")]
    // Level 3
    public MeshRenderer[] water;
    public Color[] waterColors = new Color[4];
    public Material stopper;
    public GameObject key;
    public MeshRenderer firstAidKit;
    public MeshRenderer handle;
    public Texture[] firstAidTextures = new Texture[4];
    public Color[] handleColors = new Color[4];
    public MeshRenderer flowers;
    public Color[] flowerColors = new Color[4];
    public List<GameObject> destroyables = new List<GameObject>();
    [Header("Other")]
    public MeshRenderer spiralVoid;
    public Color[] voidColors = new Color[4];
    public List<Button> arrows = new List<Button>();
    public bool dirty;
    public float currentMode;

    //Other shaders
    [Header("Shader Names (exclude ShaderGraphs part)")]
    [SerializeField]
    public string[] shaderNames = new string[] 
    {"Cutscene1Fall","Dissolve Test1_new","GlowTest_new",
        "Hallway Walls_New","Lamp_New","TransparentBlocks"
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
        if(visionMat != null)
        {
            visionMat.SetColor("_color", visionColors[index]);
        }
        

        foreach(MeshRenderer objMesh in objectives)
        {
            if(objMesh == null)
            {
                continue;
            }
            foreach(Material mat in objMesh.materials)
            {
                if(mat == null)
                {
                    continue;
                }
                //sprint(mat.name);
                mat.SetColor("_Glow", objectiveColors[index]);
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
        if(blockPuzzleText != null)
        {
            blockPuzzleText.color = textColors[index];
        }

        if (particles.Count > 0)
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

        if(ominousLight != null)
        {
            ominousLight.color = omLightColors[index];
        }

        if(lamps.Count > 0)
        {
            foreach (MeshRenderer mesh in lamps)
            {
                mesh.material.SetColor("_glow", lampColors[index]);
            }
        }

        if(firstAidKit != null && handle != null)
        {
            firstAidKit.material.mainTexture = firstAidTextures[index];
            handle.material.color = handleColors[index];
        }

        if(toyboxParts.Count > 0)
        {
            foreach(MeshRenderer mesh in toyboxParts)
            {
                mesh.material.SetColor("_Glow", objectiveColors[index]);
            }
        }
        /*
        foreach (MeshRenderer objMesh in toyboxParts)
        {
            if (objMesh == null)
            {
                continue;
            }
            foreach (Material mat in objMesh.materials)
            {
                if (mat == null)
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
        //*/
        if(water.Length > 0)
        {
            foreach(MeshRenderer w in water)
            {
                Color wColor = w.material.color;
                wColor.r = waterColors[index].r;
                wColor.g = waterColors[index].g;
                wColor.b = waterColors[index].b;
                wColor.a = w.material.color.a;
                w.material.color = wColor;
            }
        }

        if(key != null)
        {
            foreach(MeshRenderer mesh in key.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = objectiveColors[index];
                mesh.material.SetColor("_Glow", objectiveColors[index]);
            }
            
        }

        if(flowers != null)
        {
            flowers.material.color = flowerColors[index];
        }

        if(spiralVoid != null)
        {
            spiralVoid.material.SetColor("_color", voidColors[index]);
        }

        if (posters.Count > 0)
        {
            foreach (MeshRenderer poster in posters)
            {
                poster.material.color = posterColors[index];
            }
        }

        if(arrows.Count > 0)
        {
            ColorBlock cBlock = new ColorBlock();
            cBlock.colorMultiplier = 1.0f;
            cBlock.normalColor = textColors[index];
            cBlock.highlightedColor = Color.white;
            cBlock.pressedColor = textColors[index];
            foreach (Button b in arrows)
            {
                b.colors = cBlock;
                //b.colors.normalColor = textColors[index];
            }
        }

        if(destroyables.Count > 0)
        {
            foreach (GameObject wall in destroyables)
            {
                foreach (MeshRenderer mesh in wall.GetComponentsInChildren<MeshRenderer>())
                {
                    foreach (Material mat in mesh.materials)
                    {
                        mat.SetFloat("_colorblind", currentMode);
                    }
                }
            }
        }
    }

    public void defaultMode()
    {
        currentMode = 0.0f;
        DataScript.colorblindMode = currentMode;
        dirty = true;
    }

    public void deteuranopiaMode()
    {
        currentMode = 1.0f;
        DataScript.colorblindMode = currentMode;
        dirty = true;
    }

    public void protanopiaMode()
    {
        currentMode = 2.0f;
        DataScript.colorblindMode = currentMode;
        dirty = true;
    }

    public void tritanopiaMode()
    {
        currentMode = 3.0f;
        DataScript.colorblindMode = currentMode;
        dirty = true;
    }


}
