using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{

    public GameObject[] DayNPCS;
    public GameObject[] AgroNPCS;
    public int SpawnAmout;
    

    public void SpawnDayNPCS()
    {
        for(int i=0;i<SpawnAmout;i++)
        {
            int random = Random.Range(0, DayNPCS.Length);
            GameObject npc = Instantiate(DayNPCS[random], transform.position, Quaternion.identity);


            int randomgen = Random.Range(0, 100);
            
            if(FindObjectOfType<TrustLevel>().TrustSlider.value < randomgen)
            {
                npc.GetComponent<DefaultNPC>().is_distrustful = true;
                npc.GetComponent<DefaultNPC>().Sus.SetActive(true);
            }

        }

    }

    public void SpawnNightNPCS()
    {
        for (int i = 0; i < SpawnAmout; i++)
        {
           
            int randomgen = Random.Range(0, 25);

            if (FindObjectOfType<TrustLevel>().TrustSlider.value > randomgen)
            {

                int random = Random.Range(0, AgroNPCS.Length);
                GameObject npc = Instantiate(AgroNPCS[random], transform.position, Quaternion.identity);
            }

        }

    }



}
