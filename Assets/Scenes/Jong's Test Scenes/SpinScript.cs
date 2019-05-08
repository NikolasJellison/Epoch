using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public Transform[] children;
    public float angle = 1;
    public float maxAngle = 60;
    public float timeLeft = 3.8f;
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
        if(timeLeft < (1.5f))
        {
            angle = Mathf.Max(0, angle - 100 * Time.deltaTime);
        } else
        {
            angle = Mathf.Min(maxAngle, angle + 80 * Time.deltaTime);
        }
        timeLeft -= Time.deltaTime;
        
        foreach(Transform child in children)
        {
            child.RotateAround(transform.position, transform.forward, angle);
        }
    }
}
