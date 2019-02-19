using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
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

        Transform sightCone = gameObject.transform.GetChild(0); // The first child of the monster must be a cone
        Bounds sightBounds = sightCone.GetComponent<MeshCollider>().bounds;
        Bounds playerBounds = player.GetComponent<CapsuleCollider>().bounds;

        if (sightBounds.Intersects(playerBounds))
        {
            RaycastHit contact;
            // The cone must be in Ignore Raycast layer
            int layerMask = 1 << 2;
            layerMask = ~layerMask;
            if (Physics.Raycast(gameObject.transform.position, player.position - gameObject.transform.position, out contact, Mathf.Infinity, layerMask))
            {
                if(contact.transform.tag == "Player")
                {
                    if (alertLevel < 100)
                    {
                        alertLevel += 2;
                    }
                }
            }
            
        }
        else if(alertLevel > 0)
        {
            alertLevel -= 1;
        }

        /*Bounds enemyBounds = gameObject.transform.GetComponent<BoxCollider>().bounds;
        if (enemyBounds.Intersects(playerBounds))
        {
            print("OOF");
            alertLevel = 100;
        }
        //*/
        if (alertLevel >= 100)
        {
            print("oh no");
        }
        
        //print(alertLevel);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("OOF");
            //alertLevel = 100;
        }
        
    }
}