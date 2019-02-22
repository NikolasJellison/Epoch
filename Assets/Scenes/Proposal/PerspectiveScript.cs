using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveScript : MonoBehaviour
{

    public Transform player;
    public float zAngle;
    public float zTarget;
    public float zLeeway;
    public bool active;
    public Material visionMat;
    public Material realMat;
    public Transform groundBase;
    public GameObject marker;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(groundBase == null)
        {
            groundBase = transform;
        }
        gameObject.GetComponent<MeshRenderer>().material = visionMat;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toObject = groundBase.position - player.position;
        Debug.DrawRay(player.position, toObject, Color.green);
        zAngle = Vector3.SignedAngle(toObject, groundBase.forward, groundBase.up);

        float zDiff = angleDiff(zAngle, zTarget);
        
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;
        color.a = Mathf.Max(0.1f, 0.85f * ((180.0f - zDiff)/180.0f));
        gameObject.GetComponent<MeshRenderer>().material.color = color;

        if (zDiff < zLeeway)
        {
            active = true;
        } else
        {
            active = false;
        }
    }
    
    private float angleDiff(float angle, float target)
    {
        float diff = (target - angle);
        if (diff > 180.0f)
        {
            diff -= 360.0f;
        }
        else if (diff < -180.0f)
        {
            diff += 360.0f;
        }
        diff = Mathf.Abs(diff);

        return diff;
    }

    private void OnDisable()
    {
        marker.SetActive(false);
    }
}
