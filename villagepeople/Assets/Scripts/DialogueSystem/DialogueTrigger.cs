using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

   public float timeOut;

    private void Update()
    {
        if (timeOut > 0)
        {
            timeOut -= Time.deltaTime;
        }
       
    }

    public void TriggerDialogue()
    {
        
        if(timeOut <= 0)
        {
          
            DefaultNPC ai = null;

            if (TryGetComponent(out ai))
            {
                ai.TalkingFunc();

            }

            
            FindObjectOfType<DialogueSystem>().StartDialogue(dialogue, ai);
        }
       
    }
}
