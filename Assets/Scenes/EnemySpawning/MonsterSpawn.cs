using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Transform enemy;
    public Transform end;
    public Transform start;
    public float respawnTime;
    public float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        enemy.position = start.position;
        // Assign rotation?
        enemy.gameObject.SetActive(true);
        timeLeft = respawnTime;
        // Move enemy to start position, make sure it is active
        // Probably make that a function?
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.gameObject.activeInHierarchy)
        {
            float distance = Vector3.Distance(enemy.position, end.position);
            // Get distance from 
            print(distance);
            if (distance <= 2.0f)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0.0f)
            {
                enemy.gameObject.SetActive(true);
                enemy.position = start.position;
                timeLeft = respawnTime;
            }
        }

    }
}
