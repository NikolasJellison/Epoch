using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Transform[] children;
    public float angle = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in children)
        {
            child.RotateAround(transform.position, transform.forward, angle);
        }
    }
}
