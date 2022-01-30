using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TrustLevel : MonoBehaviour
{
   public Slider TrustSlider;
    [SerializeField] GameObject textPopUp;
    [SerializeField] Image fill;

    private void Start()
    {
       
        
    }
    public void TrustAction(int amout)
    {
       GameObject text = Instantiate(textPopUp, transform.position, Quaternion.identity);
        
        if(amout < 0)
        {
            text.GetComponentInChildren<Text>().text = "-" + amout.ToString();
            text.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            text.GetComponentInChildren<Text>().text = "+" + amout.ToString();

            text.GetComponentInChildren<Text>().color = Color.green;
        }
        TrustSlider.value += amout;
        fill.color = new Color( (80 - TrustSlider.value) / 100,  (20 + TrustSlider.value) / 100, 0);

    }
    public void SilentAction(int amout)
    {
        TrustSlider.value += amout;
    }

  
}
