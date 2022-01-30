using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAI : MonoBehaviour
{
  [SerializeField] NavMeshAgent agent;
    [SerializeField] float runDistance;
    Transform playerPos;
    float featherdelay;
   [SerializeField] GameObject featherObject;
    [SerializeField] Animator animator;
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector3.Distance(playerPos.position,transform.position) < runDistance)
        {
            
            animator.SetBool("move", true);
            Vector3 dirToPlayer = transform.position - playerPos.position;

            Vector3 newpos = transform.position + dirToPlayer;
            
            if(featherdelay <= 0)
            {
                Instantiate(featherObject, transform.position, Quaternion.identity);
                featherdelay = 3;
            }

            agent.SetDestination(newpos);
        }
        else
        {
          

            featherdelay -= Time.deltaTime;
        }

        if (agent.remainingDistance == 0)
        {
            animator.SetBool("move", false);
        }
        
    }
}
