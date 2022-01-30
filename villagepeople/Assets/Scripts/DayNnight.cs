using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;



public class DayNnight : MonoBehaviour
{
    public Transform sun;
    public Text text;
    public float TimeMultiplier = 2.4f;
    private float StartTime = 12f;
    public float rotIncrease=1;
    public Volume NightVolume,DayVolume;

    private DateTime TimeNow;
    public bool ItsDay, ItsNight;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        TimeNow = DateTime.Now.Date + TimeSpan.FromHours(StartTime);
        sun.eulerAngles = new Vector3(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter();

        if (sun.eulerAngles.x > 200 && ItsNight == false)
        {
            Night();
            ItsNight = true;
            ItsDay = false;
        }
        else if (sun.eulerAngles.x < 200 && ItsDay == false)
        {
            Day();
            ItsDay = true;
            ItsNight = false;
        }

        if (sun.transform.eulerAngles.x > 200 && sun.transform.eulerAngles.x < 350)
        {
            NightVolume.weight = Mathf.Lerp(NightVolume.weight, 1, .3f * Time.deltaTime);

            DayVolume.weight = Mathf.Lerp(DayVolume.weight, 0, .4f * Time.deltaTime);

          
        }
        else
        {
            NightVolume.weight = Mathf.Lerp(NightVolume.weight, 0, .3f * Time.deltaTime);

            DayVolume.weight = Mathf.Lerp(DayVolume.weight, 1, .4f * Time.deltaTime);
        }

        if(ItsNight)
        {
            FindObjectOfType<DialogueInteractor>().active = false;
        }

    }

    private void TimeCounter()
    {
       // text.text = TimeNow.ToString("HH:mm");
        sun.Rotate(Time.deltaTime / 1.666666666666667f * rotIncrease, 0f, 0f);
    }

    private void Night()
    {
        Debug.Log("its night");

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        for(int i=0;i<npcs.Length;i++)
        {
            npcs[i].GetComponent<DefaultNPC>().BackHome() ;
        }

        FindObjectOfType<NPCSpawner>().SpawnNightNPCS();


    }
    private void Day()
    {
        if(!active)
        {
            active = true;

        }else
        {
            FindObjectOfType<DialogueInteractor>().active = true;


            FindObjectOfType<NPCSpawner>().SpawnDayNPCS();

        }
       
    }
}
