using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempOpenScript : MonoBehaviour
{
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!open)
        {
            open = true;
            gameObject.GetComponent<Animator>().SetTrigger("OpenOut");
        }
    }
}
