using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonLightDetection : MonoBehaviour
{
    private Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        mat.SetVector("_SetLightDirection", -this.transform.forward);
    }
}
