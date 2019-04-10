using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurtainScript : MonoBehaviour
{

    public Text moveText;
    public float translation;
    public float tLimit;
    public float speed;
    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        moveText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(start && translation < tLimit)
        {
            Vector3 pos = transform.position;
            pos.z += Time.deltaTime * speed;
            transform.position = pos;
            translation += Time.deltaTime * speed;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!start)
            {
                moveText.text = "'E' to move aside";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    start = true;
                }
            }
            else
            {
                moveText.text = "";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveText.text = "";
        }
    }
}
