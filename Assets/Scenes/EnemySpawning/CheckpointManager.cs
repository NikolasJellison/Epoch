using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] checkpoints;
    public Transform player;
    public GameObject[] enemies;
    public Transform currentCheckpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        if(checkpoints.Length > 0)
        {
            currentCheckpoint = checkpoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoints.Length > 0)
        {
            float closestDist = Vector3.Distance(player.position, checkpoints[0].position);
            Transform bestPoint = checkpoints[0];

            for (int i = 1; i < checkpoints.Length; ++i)
            {
                float currentDist = Vector3.Distance(player.position, checkpoints[i].position);
                if (currentDist <= closestDist)
                {
                    closestDist = currentDist;
                    bestPoint = checkpoints[i];
                }
            }


            if (closestDist <= 15.0f)
            {
                currentCheckpoint = bestPoint;
            }
        }

        int highestAlert = enemies[0].GetComponent<EnemyBehavior>().alertLevel;
        for(int i = 1; i < enemies.Length; ++i)
        {
            if(highestAlert >= 100)
            {
                break;
            }
            int currentAlert = enemies[i].GetComponent<EnemyBehavior>().alertLevel;
            if (highestAlert < currentAlert)
            {
                highestAlert = currentAlert;
            }
        }

        if(player.position[1] < -50.0f || highestAlert >= 100)
        {
            if (checkpoints.Length > 0)
            {
                player.position = currentCheckpoint.position;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
