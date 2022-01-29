using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    public string name;
    public AudioClip voiceNormal;

    public bool YesPromptCorrect;

    [TextArea(3,10)]
    public string[] sentences;

   
}
