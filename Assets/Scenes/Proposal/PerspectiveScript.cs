using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveScript : MonoBehaviour
{ 
    public Transform objBase;
    public Transform player;
    public Vector3 ray;
    public Vector3 targetRay;
    public float leeway;
    public float diff;
    public bool active;
    public float distance;
    public float targetDist;
    public float distLeeway;
    public Material visionMat;
    public Material[] realMats;
    public GameObject marker;
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
            print(objT.childCount);

            for (int i = 0; i < objT.childCount; ++i)
            {
                Transform child = objT.GetChild(i);
                GameObject childObj = child.gameObject;
                MeshRenderer mesh = childObj.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    realMats[i] = Instantiate(mesh.material);
                    print(realMats[i]);
                    mesh.material = visionMat;
                }
                MeshCollider meshCol = childObj.GetComponent<MeshCollider>();
                if (meshCol != null)
                {
                    meshCol.isTrigger = true;
                }

                BoxCollider boxCol = childObj.GetComponent<BoxCollider>();
                if (boxCol != null)
                {
                    boxCol.isTrigger = true;
                }
            }
        }

        if (objBase == null)
        {
            objBase = transform;
        }
        //gameObject.GetComponent<MeshRenderer>().material = visionMat;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Vector3.Normalize(objBase.position - player.position);
        distance = Vector3.Distance(objBase.position, player.position);
        diff = Vector3.Angle(ray, targetRay);

        Color color = visionMat.color;
        color.a = Mathf.Max(0.1f, 0.85f * ((180.0f - diff) / 180.0f));

        Transform objT = gameObject.transform;
        for (int i = 0; i < objT.childCount; ++i)
        {
            Transform child = objT.GetChild(i);
            GameObject childObj = child.gameObject;
            MeshRenderer mesh = childObj.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material.color = color;
            }
        }

        //gameObject.GetComponent<MeshRenderer>().material.color = color;
        
        if (diff < leeway && Mathf.Abs(distance - targetDist) < distLeeway)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }


    private void OnDisable()
    {
        

        if(moveable)
        {
            gameObject.tag = "Move_Able";
        }

        foreach(GameObject obj in activateObjs)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in deactivateObjs)
        {
            obj.SetActive(false);
        }
        marker.SetActive(false);

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
                    meshCol.isTrigger = false;
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
    }
}
