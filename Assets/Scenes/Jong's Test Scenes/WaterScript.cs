using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float translation;
    public float tSpeed;
    public float tLimit;
    public float aSpeed;
    public float albedoLimit;
    public bool startClear;
    public GameObject waterFlow;
    public BoxCollider key;
    public GameObject stopper;
    bool keyActivated;
    bool played;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!played && stopper != null && !stopper.GetComponent<VisionObjectScript>().enabled)
        {
            played = true;
            GetComponent<AudioSource>().Play();
        }
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Color color = mesh.material.color;
        if(startClear & color.a > albedoLimit)
        {
            if(color.a < albedoLimit + .2f)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
     
            color.a -= Time.deltaTime * aSpeed;
            GetComponent<MeshRenderer>().material.color = color;
        }

        if(waterFlow.activeSelf && color.a <= albedoLimit)
        {
            print("END: " + Time.fixedTime);
            waterFlow.SetActive(false);
        }
        if(stopper != null && !stopper.GetComponent<VisionObjectScript>().enabled && translation < tLimit)
        {
            Vector3 pos = transform.position;
            pos.y -= Time.deltaTime * tSpeed;
            transform.position = pos;
            translation += Time.deltaTime * tSpeed; 
        }
        if(translation > .5 && !keyActivated)
        {
            key.enabled = true;
            keyActivated = true;
        }
    }

    
}
