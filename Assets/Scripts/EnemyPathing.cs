using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyPathing : MonoBehaviour
{

    public Transform[] goal;
    public Transform player;
    public int currentGoal;
    public int alertLevel;

    void Start()
    {
        
        currentGoal = 0;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal[currentGoal].position;
    }

    private void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent.remainingDistance <= 1)
        {
            currentGoal = (currentGoal + 1) % goal.Length;
        }

        agent.destination = goal[currentGoal].position;

        Transform sightCone = gameObject.transform.GetChild(0);
        Bounds sightBounds = sightCone.GetComponent<MeshCollider>().bounds;
        Bounds playerBounds = player.GetComponent<CapsuleCollider>().bounds;

        

        if (sightBounds.Intersects(playerBounds))
        {
            if(alertLevel < 100)
            {
                alertLevel += 2;
            }
        }
        else if(alertLevel > 0)
        {
            alertLevel -= 1;
        }

        Bounds enemyBounds = gameObject.transform.GetComponent<BoxCollider>().bounds;
        if (enemyBounds.Intersects(playerBounds))
        {
            print("OOF");
            alertLevel = 100;
        }

        if (alertLevel >= 100)
        {
            print("oh no");
        }
        print(alertLevel);
    }
}