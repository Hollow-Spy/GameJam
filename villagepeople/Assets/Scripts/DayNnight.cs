using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNnight : MonoBehaviour
{
    public Transform sun;
    public Text text;
    private float TimeMultiplier = 2.4f;
    private float StartTime = 12f;
    private DateTime TimeNow;
    private bool ItsDay, ItsNight;
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

        if (TimeNow.Hour == 20 && ItsNight == false)
        {
            Night();
            ItsNight = true;
            ItsDay = false;
        }
        else if (TimeNow.Hour == 8 && ItsDay == false)
        {
            Day();
            ItsDay = true;
            ItsNight = false;
        }
    }

    private void TimeCounter()
    {
        TimeNow = TimeNow.AddMinutes(Time.deltaTime * TimeMultiplier);
        text.text = TimeNow.ToString("HH:mm");
        sun.Rotate(Time.deltaTime / 1.666666666666667f, 0f, 0f);
    }

    private void Night()
    {
        Debug.Log("its night");
    }
    private void Day()
    {
        Debug.Log("its day");
    }
}
