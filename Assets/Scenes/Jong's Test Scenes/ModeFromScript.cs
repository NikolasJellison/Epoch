using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ModeFromScript : MonoBehaviour
{
    public TMP_Dropdown list;
    // Start is called before the first frame update
    void Start()
    {
        list.value = (int)DataScript.colorblindMode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
