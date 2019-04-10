using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnabler : MonoBehaviour
{
    public GameObject trigger;
    public GameObject[] remove;
    public GameObject[] add;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        foreach(GameObject obj in remove)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in add)
        {
            obj.SetActive(true);
        }
    }
}
