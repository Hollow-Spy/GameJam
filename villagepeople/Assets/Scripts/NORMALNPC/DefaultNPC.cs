using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefaultNPC : MonoBehaviour
{
    Transform[] SpawnPositions;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] bool is_home,is_wander,is_talking;
    [SerializeField] bool neutral;
   public bool is_distrustful,is_afraid;
    public GameObject Sus,Alert;
    Transform playerPos;
    public float delay=2;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPositions = GameObject.Find("SpawnPositions").GetComponentsInChildren<Transform>();

        transform.position = SpawnPositions[Random.Range(0,SpawnPositions.Length) ].position;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!is_home && !is_wander && !is_talking) 
        {
            is_wander = true;
            StartCoroutine(wanderNumerator());

        }
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }

        if(is_afraid && !is_home)
        {
            Sus.SetActive(false);
            Alert.SetActive(true);

            if (Vector3.Distance(playerPos.position, transform.position) < 14)
            {

                agent.speed = 14;
                //animator.SetBool("move", true);
                Vector3 dirToPlayer = transform.position - playerPos.position;

                Vector3 newpos = transform.position + dirToPlayer;

              

                agent.SetDestination(newpos);
            }
            else
            {
                agent.speed = 8;
            }
        }
        else
        {
            agent.speed = 3.5f;
        }

    }
    public void Trust()
    {
        if (!neutral)
        {
            is_afraid = false;
            is_distrustful = false;


            Sus.SetActive(false);
            Alert.SetActive(false);


          
        }
    }



    public void Distrust()
    {
        if(!neutral)
        {
            if(is_distrustful)
            {
                is_afraid=true;
                Sus.SetActive(false);
                Alert.SetActive(true);
            }
            else
            {
                is_distrustful = true;
                Sus.SetActive(true);

            }

            Debug.Log("print");

            


            FindObjectOfType<TrustLevel>().SilentAction( Random.Range(-1,-3));




        }

    }

  
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("NPC") && is_distrustful && !is_talking && delay <= 0 && !is_home)
        {
          
            StopAllCoroutines();

            StartCoroutine(TalkingTrash(other));

            delay = 7;

            other.GetComponent<DefaultNPC>().TalkingFunc();
            TalkingFunc();

          

        }
    }

    IEnumerator TalkingTrash( Collider other)
    {
        yield return new WaitForSeconds(3);
        if(is_distrustful )
        {
            is_afraid = true;
        }
        else
        {
            is_distrustful = true;
        }
       
        other.GetComponent<DefaultNPC>().Distrust();
        other.GetComponent<DefaultNPC>().delay = 3;

        other.GetComponent<DefaultNPC>().NotTalkingFunc();

        NotTalkingFunc();

    }

    public void TalkingFunc()
    {
        GetComponent<DialogueTrigger>().timeOut = 15;

        agent.isStopped =true;
        is_talking = true;
        is_wander = false;
        
    }
    public void NotTalkingFunc()
    {
        agent.isStopped = false;

        is_talking = false;
    }
 
    IEnumerator wanderNumerator()
    {
        while(is_wander && !is_home)
        {
            if(is_distrustful)
            {
                int rand = Random.Range(0, 4);

                if(rand == 0)
                {
                   

                    GameObject[] NPCS = GameObject.FindGameObjectsWithTag("NPC");
                    int i = Random.Range(0,NPCS.Length);

                    
                        yield return new WaitForSeconds(.5f);

                         while (agent.remainingDistance > 2 && NPCS[i].transform )
                    {
                        agent.SetDestination(NPCS[i].transform.position);

                        yield return null;

                    }
                         


                }



            }
           


            Vector3 randmpos = new Vector3(Random.Range(-41.7999992f, 21.1000004f), -1.73000002f, Random.Range(-25.1000004f, 55.2999992f));

            agent.SetDestination(randmpos);

            yield return new WaitForSeconds(.5f);

            while (agent.remainingDistance > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(2, 5));

        }

       
    }


    public void BackHome()
    {
        StopAllCoroutines();
        StartCoroutine(BackHomeNumerator() );
    }

    IEnumerator BackHomeNumerator()
    {
        is_home = true;
       
        Transform targethouse = SpawnPositions[Random.Range(0, SpawnPositions.Length)];
        agent.SetDestination(targethouse.position);
        yield return new WaitForSeconds(.5f);

        if (is_distrustful)
        {
            agent.speed = 15;
        }
        while (agent.remainingDistance > .5f )
        {
            
            agent.isStopped = false;
          
           
            agent.SetDestination(targethouse.position);
            yield return null;
        }

        Destroy(gameObject);
        


    }

}
