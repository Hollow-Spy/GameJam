using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskInteract : MonoBehaviour
{
    bool maskOn;
    [SerializeField] GameObject PressInfo,RoarEffect;
    DayNnight sun;
    [SerializeField] Animator maskanimator;
    [SerializeField]  AudioSource music;
    [SerializeField] AudioClip daymusic, nightmusic;
   
    private void Start()
    {
        // 0.202
        sun = FindObjectOfType<DayNnight>();
    }

    IEnumerator NightTheme()
    {
        while(music.volume > 0)
        {
            music.volume -= .1f * Time.deltaTime;
            yield return null;
            music.pitch -= .2f * Time.deltaTime;
        }
        music.clip = nightmusic;
        music.Play();
        music.pitch = 1;

        while (music.volume < .202f)
        {
            music.volume += .1f * Time.deltaTime;
            yield return null;
           
        }


    }
    IEnumerator DayTheme()
    {

        while (music.volume > 0)
        {
            music.volume -= .1f * Time.deltaTime;
            yield return null;
            music.pitch -= .2f * Time.deltaTime;
        }
        music.clip = daymusic;
        music.Play();
        music.pitch = 1;

        while (music.volume < .202f)
        {
            music.volume += .1f * Time.deltaTime;
            yield return null;

        }

    }

    private void Update()
    {
        if(sun.ItsNight && !maskOn)
        {
            PressInfo.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.F))
            {
                FindObjectOfType<PlayerCombat>().active = true;

                Instantiate(RoarEffect, transform.position, Quaternion.identity);
                StartCoroutine(NightTheme());
                maskOn = true;
                maskanimator.Play("MaskAnim");
                PressInfo.SetActive(false);


            }
        }

       
     

        if(sun.ItsDay)
        {
            PressInfo.SetActive(false);

            if(maskOn)
            {
                FindObjectOfType<PlayerCombat>().active = false;

                FindObjectOfType<DialogueInteractor>().active = true;

                StartCoroutine(DayTheme());
                maskOn = false;
                maskanimator.Play("MaskOut");


            }

        }



    }


}
