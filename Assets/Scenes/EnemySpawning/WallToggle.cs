using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallToggle : MonoBehaviour
{
    public GameObject[] walls;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            foreach(GameObject wall in walls)
            {
                wall.SetActive(true);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }
}
