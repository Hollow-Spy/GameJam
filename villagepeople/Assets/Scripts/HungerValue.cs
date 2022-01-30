using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HungerValue : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject crunch;
    float delay;

    private void Update()
    {

        if(delay < 0)
        {
            delay = 9;
            slider.value -= 1;
        }
        else
        {
            delay -= Time.deltaTime;
        }

        if(slider.value <= 0)
        {
            Application.Quit();
        }
    }

    public void GainHunger(int amout)
    {
        slider.value += amout;

     if(amout < 0)
        {
            Instantiate(blood, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
        }
     else
        {
            Instantiate(crunch, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);

        }


    }

}
