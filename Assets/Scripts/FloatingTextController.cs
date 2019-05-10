/*
* Author : Genimoz
* Copyright (c) 2018 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour
{
    static FloatingText popUpText;
    static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("FloatingTextCanvas");
        popUpText = Resources.Load<FloatingText>("PopUpText");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popUpText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
