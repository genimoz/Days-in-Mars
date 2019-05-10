/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject loader;
    Animator anim;
    AudioSource buttonClicked;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isReady", true);
        buttonClicked = GameObject.Find("StartButton").GetComponent<AudioSource>();
    }

    public void StartPlay()
    {
        buttonClicked.Play();
        if(buttonClicked.isPlaying)
        {
            SceneManager.LoadScene("Mars", LoadSceneMode.Single);
        }
    }

}
