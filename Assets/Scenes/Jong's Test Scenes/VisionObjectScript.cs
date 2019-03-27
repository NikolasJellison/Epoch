using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionObjectScript : MonoBehaviour
{
    public Material visionMat;
    public Material[] realMats;
    public GameObject[] activateObjs;
    public GameObject[] deactivateObjs;
    public bool moveable;
    public bool remove;
    // Start is called before the first frame update
    void Start()
    {
        if (!remove)
        {
            Transform objT = gameObject.transform;
            realMats = new Material[objT.childCount];
            //print(objT.childCount);

            for (int i = 0; i < objT.childCount; ++i)
            {
                Transform child = objT.GetChild(i);
                GameObject childObj = child.gameObject;
                MeshRenderer mesh = childObj.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    realMats[i] = Instantiate(mesh.material);
                    //print(realMats[i]);
                    mesh.material = visionMat;
                }
                MeshCollider meshCol = childObj.GetComponent<MeshCollider>();
                if (meshCol != null)
                {
                    if (meshCol.convex)
                    {
                        meshCol.isTrigger = true;
                    }
                    else
                    {
                        meshCol.enabled = false;
                    }
                }

                BoxCollider boxCol = childObj.GetComponent<BoxCollider>();
                if (boxCol != null)
                {
                    boxCol.isTrigger = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (!remove)
        {
            Transform objT = gameObject.transform;
            for (int i = 0; i < objT.childCount; ++i)
            {
                Transform child = objT.GetChild(i);
                GameObject childObj = child.gameObject;
                MeshRenderer mesh = childObj.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    mesh.material = realMats[i];

                }
                MeshCollider meshCol = childObj.GetComponent<MeshCollider>();
                if (meshCol != null)
                {
                    if (meshCol.convex)
                    {
                        meshCol.isTrigger = false;
                    }
                    else
                    {
                        meshCol.enabled = true;
                    }
                }

                BoxCollider boxCol = childObj.GetComponent<BoxCollider>();
                if (boxCol != null)
                {
                    boxCol.isTrigger = false;
                }
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (moveable)
        {
            gameObject.tag = "Move_Able";
        }

        foreach (GameObject obj in activateObjs)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        foreach (GameObject obj in deactivateObjs)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
