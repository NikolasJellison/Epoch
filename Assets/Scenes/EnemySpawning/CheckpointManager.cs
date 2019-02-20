﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] checkpoints;
    public Transform player;
    public GameObject[] enemies;
    public Transform currentCheckpoint;
    public GameObject watch;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = checkpoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        float closestDist = Vector3.Distance(player.position, checkpoints[0].position);
        Transform bestPoint = checkpoints[0];

        for(int i = 1; i < checkpoints.Length; ++i)
        {
            float currentDist = Vector3.Distance(player.position, checkpoints[i].position);
            if(currentDist <= closestDist)
            {
                closestDist = currentDist;
                bestPoint = checkpoints[i];
            }
        }

        if(closestDist <= 15.0f)
        {
            currentCheckpoint = bestPoint;
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

        if(player.position[1] < -100.0f || highestAlert >= 100)
        {
            player.position = currentCheckpoint.position;
        }

        if(watch.GetComponent<Watch_Vision>().time_left <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}