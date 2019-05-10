/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public int playerFoodPoint = 50;
	public float enemyMoveDelay = 0.1f;

	public float startLevelDelay = 1f;

	[HideInInspector] public bool playerCanMove = true;

	private BoardManager boardScript;
    private Highscore highscoreScript;
	[HideInInspector] public int level = 0;

	private List<Enemy> enemies;
	private bool areEnemiesMoving;

	private Text levelText; // Text to be displayed on loading screen
    private Text levelTextGameplayUI; // Text to be displayed during gameplay
    private Text highscoreText;
	private GameObject loadingScreen;
    private GameObject keypadButtonCanvas;
    private GameObject gameOverPanel;

	private bool isDoingSetup;

	void Awake ()
	{
		if(instance == null)
		{
			instance = this;
		}

		else if(instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);

		boardScript = GetComponent<BoardManager>();
        highscoreScript = GetComponent<Highscore>();
		enemies = new List<Enemy>();

		InitializeGame();
	}

	void Update ()
	{
		if(playerCanMove || areEnemiesMoving || isDoingSetup)
		{
			return;
		}

		StartCoroutine(MoveEnemies());
	}

	public void AddEnemyToList(Enemy script)
	{
		enemies.Add(script);
	}

	IEnumerator MoveEnemies()
	{
		areEnemiesMoving = true;
		yield return new WaitForSeconds(enemyMoveDelay);

		if(enemies.Count == 0)
		{
			yield return new WaitForSeconds(enemyMoveDelay);
		}

		for(int i = 0; i < enemies.Count; i++)
		{
			enemies[i].MoveEnemy();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playerCanMove = true;
		areEnemiesMoving = false;
	}

	public void GameOver()
	{
        gameOverPanel.SetActive(true);
		levelText.text = "Your discovery on Mars ends at SOL " + level;
		loadingScreen.SetActive(true);
		enabled = false;
	}

	public void InitializeGame()
	{
		isDoingSetup = true;

        // Loading Screen
		loadingScreen = GameObject.Find("LoadingScreen");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        // Game Over Button (disabled when the Game is not over yet)
        GameObject loadingScreenCanvas = GameObject.Find("LoadingScreenCanvas"); // Get reference to its parent object
        gameOverPanel = loadingScreenCanvas.transform.Find("GameOverPanel").gameObject; // Get the gameobject in child

        levelText.text = "SOL " + level;
        loadingScreen.SetActive(true);
        gameOverPanel.SetActive(false); // Disable Game Over button

        keypadButtonCanvas = GameObject.Find("Virtual Keypad");
        //keypadButtonCanvas.SetActive(false); // Deactivate Keypad

		Invoke("HideLoadingScreen", startLevelDelay);

		enemies.Clear();
		boardScript.SetupScene(level);

        // Check if the current SOL completed is greater than current longest SOL
        if(level > PlayerPrefs.GetInt("Current SOL", 1))
        {
            PlayerPrefs.SetInt("Current SOL", level); // then set the Highscore Value
        }
    }
    
    //	public void Restart()
    //	{
    //		SceneManager.LoadScene(0);
    //		Debug.Log("Reload Scene");
    //	}

    void HideLoadingScreen()
	{
        // Displayed during gameplay
        levelTextGameplayUI = GameObject.Find("SolNumberText").GetComponent<Text>();
        levelTextGameplayUI.text = level.ToString();

        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        highscoreText.text = highscoreScript.highScore.ToString();

        loadingScreen.SetActive(false);
        gameOverPanel.SetActive(false); // Disable Game Over button
        keypadButtonCanvas.SetActive(true); // Activate Keypad

        isDoingSetup = false;
    }

    void ShowVirtualKeypad()
    {
        keypadButtonCanvas.SetActive(true);
    }
}
