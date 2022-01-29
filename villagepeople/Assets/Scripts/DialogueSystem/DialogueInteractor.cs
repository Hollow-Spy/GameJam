using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractor : MonoBehaviour
{
  public bool active;
    

   
    void Update()
    {
        if(active && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
           if( Physics.Raycast(transform.position, transform.forward, out hit, 2) )
            {
                if(hit.collider.gameObject.CompareTag("NPC"))
                {
                   hit.collider.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                }


            }


        }
    }
}
