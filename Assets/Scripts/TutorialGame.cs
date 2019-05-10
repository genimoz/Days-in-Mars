/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGame : MonoBehaviour
{
    GameManager instance;

    Player playerScript;
    GameObject dialogueManagerGO;
    DialogueTrigger trigger;

    public bool isTutorialCompleted;

	void Start ()
	{
        // Get reference to Player Game Object
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGameObject.GetComponent<Player>();

        // Get reference to Dialogue Manager Game Object
        dialogueManagerGO = GameObject.Find("DialogueManager");

        // Get reference Dialogue Trigger script
        GameObject dTrigger;
        if(GameManager.instance.level == 1)
        {
            dTrigger = GameObject.Find("DialogueTrigger1");
            trigger = dTrigger.GetComponent<DialogueTrigger>();
        }
        if(GameManager.instance.level == 2)
        {
            dTrigger = GameObject.Find("DialogueTrigger2");
            trigger = dTrigger.GetComponent<DialogueTrigger>();
        }

        //// Get reference Dialogue Trigger script
        //GameObject dTrigger = GameObject.Find("DialogueTrigger");
        //trigger = dTrigger.GetComponent<DialogueTrigger>();
    }
	
	void Update ()
	{
        if(playerScript.isPlayerMoving && !isTutorialCompleted)
        {
            ShowTutorial();
            //GameManager.instance.enabled = false;
        }
        else if(isTutorialCompleted)
        {
            //GameManager.instance.enabled = true;
        }
	}

    void ShowTutorial()
    {
        dialogueManagerGO.SetActive(true);
        trigger.TriggerDialogue();
    }
}
