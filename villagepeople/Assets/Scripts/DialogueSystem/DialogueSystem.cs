using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public bool talking;
    public bool dialoguepause;
    public Queue<string> sentences;
    [SerializeField] Animator backgroundAnimator;
    [SerializeField] Text dialogueText;
    [SerializeField] Text nameText;
    [SerializeField] AudioSource voiceplayer;

    void Start()
    {
  
        sentences = new Queue<string>();
    }


    void Update()
    {
        if (talking && Input.GetMouseButton(0) && dialoguepause)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        FindObjectOfType<DialogueInteractor>().active = false;

        talking = true;
        nameText.text = dialogue.name;
        voiceplayer.clip = dialogue.voiceNormal;

        backgroundAnimator.Play("PopUp");
     


        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(SentenceNumerator(sentence) );
    }

    IEnumerator SentenceNumerator(string sentence)
    {
        
        dialogueText.text = "";
        dialoguepause = false;
        backgroundAnimator.SetBool("talking", true);


        int index =0;
        bool quiet=false;
        bool loud = false;
        float soundDelay=0;
        float basedelay;
        float textDelay;
      

        while(index < sentence.Length)
        {
           
          
            dialogueText.text += sentence[index];
                
           

            soundDelay -= Time.deltaTime;
          

           
          float baseTone = 1;
            basedelay = .2f;
            if (char.IsUpper(sentence[index]) )
            {
                baseTone = 1.3f;
                basedelay = .1f;
                if (!loud)
                {
                    soundDelay = 0;
                }
                loud = true;
                

            }
            else
            {
                loud = false;

                if(sentence[index] == '*' )
                {

                    quiet = !quiet;
                    soundDelay = 0;
                }

            }
          
            if(quiet)
            {
                basedelay = .3f;
                baseTone = .8f;
            }


            if(soundDelay <= 0)
            {
                Debug.Log("play");
                voiceplayer.Play();
                voiceplayer.pitch = Random.Range(baseTone - .2f, baseTone + .2f);
                voiceplayer.time = Random.Range(0, 10);
                soundDelay = basedelay; 
                
            }
            yield return new WaitForSeconds(0.097f);
            if(sentence[index] == '.' )
            {
                yield return new WaitForSeconds(.1f);
                voiceplayer.Stop();
                yield return new WaitForSeconds(.3f);

            }
            index++;

          



        }
        backgroundAnimator.SetBool("talking", false);
        voiceplayer.Stop();
        dialoguepause = true;



    }

    public void EndDialogue()
    {
        FindObjectOfType<DialogueInteractor>().active = true;

        nameText.text = "";
        dialogueText.text = "";
        talking = false;
        backgroundAnimator.SetBool("talking", false);
        backgroundAnimator.Play("PopOut");
    }

}
