using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorblindScript : MonoBehaviour
{
    public Material[] mat;
    public int mode;
    public Material current_mat;

    void Start()
    {

    }

    private void Update()
    {
        current_mat = mat[mode];
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // src is fully rendered scene that you would normally send to the monitor
        // we are intercepting it to do some stuff before we pass it on.

        // material is being set to the source

        Graphics.Blit(source, destination, current_mat);
    }
}
