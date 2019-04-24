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
        foreach(Transform child in children)
        {
            child.parent = transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward, angle);
    }
}
