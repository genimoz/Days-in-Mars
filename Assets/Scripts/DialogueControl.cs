/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueControl : MonoBehaviour
{
    GameObject dialogueManagerGO;
    TutorialGame tutor;

	void Start ()
	{
        dialogueManagerGO = GameObject.Find("DialogueManager");
        tutor = GetComponent<TutorialGame>();
    }
	
	void Update ()
	{
        if(GameManager.instance.level <= 2)
        {
            dialogueManagerGO.SetActive(true);
        }
        else
        {
            dialogueManagerGO.SetActive(false);
            tutor.isTutorialCompleted = true;
        }
    }
}
