using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public PerspectiveSwap vantageManager;
    public bool playCutscene;
    public GameObject controller;
    public GameObject playerCam;
    public GameObject cutsceneCam;
    public GameObject UI;
    public Transform pivot;
    public float pivotDegrees;
    public Transform[] cutsceneCamLocs;
    public float delay;
    public Vector3 skipPos;
    public float prevModelDist;
    public float currModelDist1;
    public float currModelDist2;
    public float speed;
    public float transparencySpeed;
    public Vector3 prevModelDir;
    public Vector3 currModelDir1;
    public Vector3 currModelDir2;
    public GameObject prevModel;
    public float doorDist;
    public Transform prevModelTarg;
    public GameObject currModel;
    public Transform currModelTarg;
    public GameObject mist;
    public float rotationLeft;
    public GameObject door;
    bool doorOpened;
    public int stage = 0;
    public float camMoveSpeed;
    public float camRotationSpeed;
    public Transform target;
    public GameObject lightRoom;
    // Start is called before the first frame update
    void Start()
    {
        if (playCutscene)
        {
            currModel.SetActive(false);
            prevModelDir = Vector3.Normalize(prevModelDir);
            vantageManager.enabled = false;
            controller.GetComponent<PlayerController>().enabled = false;
            playerCam.SetActive(false);
            UI.SetActive(false);
        } else
        {
            cutsceneCam.SetActive(false);
            // move player to right spot
            controller.transform.position = skipPos;
            lightRoom.SetActive(false);
            prevModel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playCutscene)
        {
            return;
        }
        print(stage);
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
            stage = 0;
            CamPosition(prevModelTarg, false);
        }
        else if (prevModelDist > 0)
        {
            Vector3 playerMov = prevModelDir * speed * Time.deltaTime;
            Animator anim = prevModel.GetComponent<Animator>();

            anim.SetFloat("Velocity_X", prevModelDir.x);
            anim.SetFloat("Velocity_Y", prevModelDir.z);

            prevModel.transform.Translate(playerMov, Space.Self);
            prevModelDist -= Vector3.Magnitude(playerMov);
            stage = 1;
            CamPosition(prevModelTarg, false);
        }
        else if (stage == 1)
        {
            //deactivate prevModel
            prevModel.SetActive(false);
            //reactivate currModel
            currModel.SetActive(true);
            stage = 2;
            CamPosition(currModelTarg, false);
        }
        else if (currModelDist1 > 0 || currModelDist2 > 0)
        {
            if(currModelDist1 > 0)
            {
                Vector3 playerMov = currModelDir1 * speed * Time.deltaTime;
                Animator anim = controller.GetComponent<Animator>();

                anim.SetFloat("Velocity_X", currModelDir1.x);
                anim.SetFloat("Velocity_Y", currModelDir1.z);
                print(playerMov);
                controller.transform.Translate(playerMov, Space.Self);
                currModelDist1 -= Vector3.Magnitude(playerMov);
                doorDist -= Vector3.Magnitude(playerMov);
                if (!doorOpened && doorDist <= 0)
                {
                    doorOpened = true;
                    door.GetComponent<Animator>().SetTrigger("DoorOpenOut");
                    door.GetComponent<AudioSource>().Play();
                }
                
            }
            
            stage = 3;
            CamPosition(currModelTarg, false);
        }
        else if(stage != 4)
        {
            Animator anim = controller.GetComponent<Animator>();

            anim.SetFloat("Velocity_X", 0);
            anim.SetFloat("Velocity_Y", 0);
            // final rotation adjustment
            stage = 4;
        }
        else if (stage != 5)
        {
            Vector3 originalPosition = cutsceneCam.transform.position;
            cutsceneCam.transform.position = Vector3.MoveTowards(cutsceneCam.transform.position, playerCam.transform.position, camMoveSpeed * Time.deltaTime);

            Vector3 lookDir = currModelTarg.position - cutsceneCam.transform.position;

            Quaternion rot = Quaternion.LookRotation(lookDir, new Vector3(0, 1, 0));
            cutsceneCam.transform.rotation = Quaternion.Lerp(cutsceneCam.transform.rotation, rot, 1.5f * camRotationSpeed * Time.deltaTime);

            if (Vector3.Distance(originalPosition, cutsceneCam.transform.position) < 0.00001)
            {
                stage = 5;
            }
        }
        //*/
        if (stage < 2)
        {
            Color color = mist.GetComponent<MeshRenderer>().material.color;
            float transparency = color.a;
            transparency += transparencySpeed * Time.deltaTime;
            color.a = Mathf.Min(1, transparency);
            mist.GetComponent<MeshRenderer>().material.color = color;
        }
        else if (stage >= 2)
        {
            // decrease opacity of mist
            Color color = mist.GetComponent<MeshRenderer>().material.color;
            float transparency = color.a;
            transparency -= transparencySpeed * Time.deltaTime;
            color.a = Mathf.Max(0, transparency);
            mist.GetComponent<MeshRenderer>().material.color = color;
        }

        if(stage >= 4)
        {
            door.GetComponent<Animator>().SetTrigger("DoorOutClose");
        }
        
        if(stage == 5)
        {
            vantageManager.enabled = true;
            controller.GetComponent<PlayerController>().enabled =true;
            cutsceneCam.SetActive(false);
            playerCam.SetActive(true);
            UI.SetActive(true);
            lightRoom.SetActive(false);
            this.enabled = false;
        }
    }
    
    private void CamPosition(Transform targ, bool smoothTransition)
    {
        cutsceneCam.transform.forward = targ.position - cutsceneCam.transform.position;
        cutsceneCam.transform.position = Vector3.MoveTowards(cutsceneCam.transform.position, cutsceneCamLocs[stage].position, camMoveSpeed * Time.deltaTime);
    }
}
