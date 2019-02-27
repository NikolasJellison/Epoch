using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotScript : MonoBehaviour
{
    public bool x;
    public bool y;
    public bool z;
    public Transform[] pivotingObjs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        foreach(Transform obj in pivotingObjs)
        {
            obj.parent = transform;
        }

        if (x)
        {
            transform.Rotate(90, 0, 0);
        }
        else if(y)
        {
            transform.Rotate(0, 90, 0);
        }
        else if (z)
        {
            transform.Rotate(0, 0, 90);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
