/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // This is called only once, and the parameter tell it to be called only after the scene was loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void OnEnable()
    {
        // Register callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // This is called each time a scene is loaded
    private static void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameManager.instance.level++;
        GameManager.instance.InitializeGame();
    }
}
