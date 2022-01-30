using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{



    private bool isPaused;
    private void Start()
    {
        isPaused = false;
        canvas.enabled = false;
    }
    public Canvas canvas;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = true;
            StopTime();
            
        }

    }



    public void StopTime()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;

        }

    }

    public void ResumeTime()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1f;
            isPaused = false;
            canvas.enabled=false;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Manu()
    {
        SceneManager.LoadScene("Menu");

    }



}
