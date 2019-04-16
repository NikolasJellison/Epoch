using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColormodeScript : MonoBehaviour
{
    public List<MeshRenderer> sceneMRs;
    public List<Material> sceneMats;
    // Start is called before the first frame update
    
    void Start()
    {
        sceneMRs = new List<MeshRenderer>(GameObject.FindObjectsOfType<MeshRenderer>());
        foreach (MeshRenderer rend in sceneMRs)
        {
            foreach(Material mat in rend.materials)
            {
                if (!sceneMats.Contains(mat))
                {
                    sceneMats.Add(mat);
                    if (!(mat.shader.name == "Shader Graphs/ColorBlindTest"))
                    {
                        string message = rend.gameObject.name + ": " + mat.shader.name;
                        if(rend.transform.parent != null)
                        {
                            message = rend.transform.parent.name + "/" + message;
                        }
                        print(message);
                        

                    }
                    //print();
                }
            }
            
        }
        //sceneMats = new List<Material>(GameObject.FindObjectsOfType<Material>());
    }

    // Update is called once per frame
    void Update()
    {
        //sceneMRs[0].game
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            
            foreach(MeshRenderer mr in sceneMRs)
            {
                //mr.GetMaterials();
            }
            
        }
    //*/
    }
}
