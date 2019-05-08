using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    Rigidbody[] rb;
    public float force = 3;
    public bool dissolve;
    private float dissolveCounter;
    public float dissolveWaitTime = 4;
    public float dissolveTime;
    [Header("Empty game object where you want to destruction to go to")]
    public Transform target;
    private Vector3 forceVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        forceVector = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        /*
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            EnableDestruction();
            //foreach(Rigidbody rb in rb)
            //{
            //    rb.AddForce(Vector3.Normalize(forceVector) * force);
            //    //Destroy(rb.gameObject, 4);
            //}
            //StartCoroutine(WaitTime());
        }
        //*/

        if (dissolve)
        {
            dissolveCounter += Time.deltaTime;

            if (dissolveCounter >= dissolveTime)
            {
                dissolve = false;
                dissolveCounter = 1;

                foreach (Rigidbody obj in rb)
                {
                    Destroy(obj.gameObject);
                }
            }
            foreach(Rigidbody rb in rb)
            {
                //Debug.Log("Dissolving");
                foreach(Material m in rb.gameObject.GetComponent<MeshRenderer>().materials)
                {
                    m.SetFloat("_DissolveAmount", dissolveCounter/dissolveTime);
                }
            }
        }
    }
    public void EnableDestruction()
    {
        foreach (Rigidbody rb in rb)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.Normalize(forceVector) * force);
            //Destroy(rb.gameObject, 4);
        }
        StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(dissolveWaitTime);
        dissolve = true;
    }
}
