using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotScript : MonoBehaviour
{
    public bool x;
    public bool y;
    public bool z;
    public Transform[] pivotingObjs;
    public float angle = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        foreach(Transform obj in pivotingObjs)
        {
            if (x)
            {
                obj.RotateAround(transform.position, Vector3.right, angle);
            }
            else if (y)
            {
                obj.RotateAround(transform.position, Vector3.up, angle);
            }
            else if (z)
            {
                obj.RotateAround(transform.position, Vector3.forward, angle);
            }
        }
        /*
        foreach(Transform obj in pivotingObjs)
        {
            obj.parent = transform;
        }
        //*/
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
