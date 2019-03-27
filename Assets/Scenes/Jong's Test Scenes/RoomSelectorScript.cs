using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelectorScript : MonoBehaviour
{
    public List<GameObject> rooms;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider collision)
    {
        //print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Room"))
        {
            if (!rooms.Contains(collision.gameObject))
            {
                rooms.Add(collision.gameObject);
            }
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            if (rooms.Contains(collision.gameObject))
            {
                rooms.Remove(collision.gameObject);
            }
        }
    }
}
