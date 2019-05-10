/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    [HideInInspector]
    public int highScore;

	void Update ()
	{
        highScore = (PlayerPrefs.GetInt("Current SOL", 1)); // Get Highscore value
	}
}
