using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Script : MonoBehaviour
{
    public int pagesLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void subPage()
    {
        --pagesLeft;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pages"))
        {
            other.gameObject.SetActive(false);
            subPage();
        }
    }
}
