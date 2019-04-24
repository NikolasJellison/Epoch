using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwapScript : MonoBehaviour
{
    public PerspectiveSwap vantageManager;
    public GameObject[] roomArrows;
    public bool hideArrows;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vantageManager.playerActive)
        {
            foreach(GameObject arrow in roomArrows)
            {
                arrow.SetActive(false);
            }
        }
        else
        {
            if (!hideArrows)
            {
                foreach (GameObject arrow in roomArrows)
                {
                    arrow.SetActive(true);
                }
            }
            else
            {
                foreach (GameObject arrow in roomArrows)
                {
                    arrow.SetActive(false);
                }
            }
        }
    }
}
