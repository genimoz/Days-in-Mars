/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CnControls;

public class Player : MovingCharacter
{
	public int playerDealDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;

    public AudioClip takeOxySound;
    public AudioClip footstepSound;
    public AudioClip getHitSound;

    AudioSource audio;

	public Text foodText;

    [HideInInspector]public bool isPlayerMoving = false;
    
	Animator animator;
	int food;

    int horizontal, vertical;

	protected override void Start ()
	{
		animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
		food = GameManager.instance.playerFoodPoint;

		foodText.text = food.ToString();

		base.Start();

        FloatingTextController.Initialize();
	}
	
	void Update ()
	{
		MovePlayer();

        // Bypass level. For testing purpose only!
        if(Input.GetKey(KeyCode.T))
		{
			LevelSucceed();
		}
	}

	public void MovePlayer()
	{
		if(!GameManager.instance.playerCanMove)
		{
			return;
		}

        // Keyboard Input
        //int horizontal = (int)Input.GetAxisRaw("Horizontal");
        //int vertical = (int)Input.GetAxisRaw("Vertical");

        // For Mobile Input using CNControl
        horizontal = (int)CnInputManager.GetAxisRaw("Horizontal");
        vertical = (int)CnInputManager.GetAxisRaw("Vertical");


        if(horizontal != 0)
		{
			vertical = 0;
		}

		if(horizontal != 0 || vertical != 0)
		{
			AttemptToMove<Wall>(horizontal, vertical);
            isPlayerMoving = true;
		}

        if(horizontal == 0 && vertical == 0)
        {
            isPlayerMoving = false;
        }
	}

	private void OnDisable()
	{
		GameManager.instance.playerFoodPoint = food;
	}

	private void CheckGameOver()
	{
		if(food <= 0)
		{
            GameManager.instance.GameOver();
		}
	}

	protected override void AttemptToMove<T>(int xDirection, int yDirection)
	{
		food--;
		foodText.text = food.ToString();

		base.AttemptToMove<T>(xDirection, yDirection);

		RaycastHit2D hit;

		if(Move(xDirection, yDirection, out hit))
		{
			
		}

		CheckGameOver();
		GameManager.instance.playerCanMove = false;
        audio.PlayOneShot(footstepSound);
	}

	protected override void OnCantMove<T>(T Component)
	{
		Wall hitWall = Component as Wall;

		hitWall.DamagingWall(playerDealDamage);
		animator.SetTrigger("PlayerChop");
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Food")
		{
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + "Food: " + food;
			col.gameObject.SetActive(false);
		}

		if(col.tag == "Soda")
		{
			food += pointsPerSoda;
            //foodText.text = "+" + pointsPerSoda + "Oxygen: " + food;
            foodText.text = food.ToString();
			col.gameObject.SetActive(false);

            // Floating Combat Text
            FloatingTextController.CreateFloatingText("+" + pointsPerSoda, transform);

            audio.PlayOneShot(takeOxySound);
		}

		if(col.tag == "Exit")
		{
			Invoke("LevelSucceed", GameManager.instance.startLevelDelay);
			enabled = false;
		}
	}

	public void LoseFood(int loss)
	{
		animator.SetTrigger("PlayerGetHit");
		food -= loss;
        //foodText.text = "-" + loss + "Food: " + food;
        foodText.text = food.ToString();

        // Floating Combat Text
        FloatingTextController.CreateFloatingText("-" + loss, transform);

        audio.PlayOneShot(getHitSound);

        CheckGameOver();
	}

	void LevelSucceed()
	{
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		SceneManager.LoadScene(1); // The number of scene depends on scene index on the build settings
	}
}
