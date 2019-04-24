using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTransitionGetAlpha : MonoBehaviour
{
    private List<Renderer> r = new List<Renderer>();
    public float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        r = new List<Renderer> (GetComponentsInChildren<Renderer>());
    }
    private void Update()
    {
        foreach(Renderer r in r)
        {
            r.material.SetFloat("_alpha", alpha);
        }
    }
}
