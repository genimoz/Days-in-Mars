/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
	public GameObject gameManager;

	void Awake ()
	{
        LoadGameObject();
	}

    public void LoadGameObject()
    {
        if(GameManager.instance == null)
        {
            gameManager = GameObject.Find("GameManager");
            Instantiate(gameManager);
        }
    }
}
