using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveScript : MonoBehaviour
{ 
    public Transform player;
    public Vector3 ray;
    public Vector3 targetRay;
    public float leeway;
    public float diff;
    public bool active;
    public float distance;
    public float targetDist;
    public float distLeeway;
    public Material visionMat;
    public Material realMat;
    public GameObject marker;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = visionMat;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Vector3.Normalize(transform.position - player.position);
        distance = Vector3.Distance(transform.position, player.position);
        diff = Vector3.Angle(ray, targetRay);
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;
        color.a = Mathf.Max(0.1f, 0.85f * ((180.0f - diff) / 180.0f));
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        
        if (diff < leeway && Mathf.Abs(distance - targetDist) < distLeeway)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }


    private void OnDisable()
    {
        marker.SetActive(false);
    }
}
