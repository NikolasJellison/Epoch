using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorblindScript : MonoBehaviour
{
    public Material[] mat;
    public Material current_mat;

    void Start()
    {
        current_mat = mat[0];
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            current_mat = mat[1];
        } else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            current_mat = mat[2];
        } else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            current_mat = mat[0];
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // src is fully rendered scene that you would normally send to the monitor
        // we are intercepting it to do some stuff before we pass it on.

        // material is being set to the source

        Graphics.Blit(source, destination, current_mat);
    }
}
