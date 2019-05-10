/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;
    public Animator animator;
    
    Queue<string> sentences;

    TutorialGame tutor;

	void Start ()
	{
        sentences = new Queue<string>();

        GameObject tutorial = GameObject.Find("DialogueSystem");
        tutor = tutorial.GetComponent<TutorialGame>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        //nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        GoToNextSentence();
        GameManager.instance.playerCanMove = false;
    }

    public void GoToNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines(); // Stops animating previous sentence
        StartCoroutine(TypingSentence(sentence)); // Start animating a new one
    }

    // Make typing animation to the sentence
    IEnumerator TypingSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) // ToCharArray() converts string into a character array
        {
            dialogueText.text += letter;

            // How fast every character in the sentence will be displayed
            //yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        GameManager.instance.playerCanMove = true;
        tutor.isTutorialCompleted = true;
    }
}
