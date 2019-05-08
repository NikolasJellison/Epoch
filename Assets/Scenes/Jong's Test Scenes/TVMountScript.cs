using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVMountScript : MonoBehaviour
{
    public Transform tvPos;
    public Transform pivot;
    public float rotate;
    public float speed;
    public float rotateLimit;
    public Text moveUI;
    public bool start;
    public GameObject tvStatic;
    public GameObject cryingEmsy;
    public float timeLeft;
    public float threshold = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        cryingEmsy.SetActive(false);
        moveUI.text = "";
        rotate = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            cryingEmsy.SetActive(false);
            if (rotate < rotateLimit)
            {
                pivot.Rotate(0, 0, -speed * Time.deltaTime);
                rotate += speed * Time.deltaTime;
            }
        }
        else
        {
            if(timeLeft > 0)
            {
                cryingEmsy.SetActive(true);
                timeLeft -= Time.deltaTime;
            }
            else
            {
                cryingEmsy.SetActive(false);
                if(Random.value*100 > threshold)
                {
                    timeLeft = Random.value;
                }
            }
        }
        transform.position = tvPos.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (start || other.gameObject.GetComponent<PlayerController>().lock_movement)
        {
            moveUI.text = "";
            return;
        }
        moveUI.text = "'E' to move aside";
        if (Input.GetKeyDown(KeyCode.E))
        {
            start = true;
            tvStatic.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        moveUI.text = "";
    }
}
