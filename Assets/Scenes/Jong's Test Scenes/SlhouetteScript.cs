using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlhouetteScript : MonoBehaviour
{
    public GameObject[] vantagePoints;
    public GameObject currentVantage;
    public float degree = 0.5f;
    public float yOffset = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject vantage in vantagePoints)
        {
            if (vantage.activeSelf)
            {
                if(currentVantage != vantage)
                {
                    currentVantage = vantage;
                    Vector3 newpos = transform.position;
                    newpos.x = vantage.transform.position.x;
                    newpos.y = vantage.transform.position.y - yOffset;
                    newpos.z = vantage.transform.position.z;
                    transform.position = newpos;
                    transform.forward = vantage.transform.forward * -1;
                    Vector3 moveBackward = degree * transform.forward;
                    moveBackward.y = 0.0f;
                    transform.position += moveBackward;
                    break;
                }
                
            }
        }
    }
}
