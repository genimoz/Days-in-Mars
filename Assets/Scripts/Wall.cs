/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	public Sprite damagedSprite;
	public int durability = 3;

	SpriteRenderer spriteRenderer;

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamagingWall(int loss)
	{
		spriteRenderer.sprite = damagedSprite;
		durability -= loss;

		if(durability <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
