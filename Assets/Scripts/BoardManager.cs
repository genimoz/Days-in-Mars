/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count(int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;

	public Count wallCount = new Count(5, 9);
	public Count foodCount = new Count(1, 5);

	public GameObject playerPrefab;

	public GameObject exitSign;
	public GameObject[] floorTiles;
	public GameObject[] outerwallTiles;
	public GameObject[] innerwallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemySprites;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void InitializeList()
	{
		gridPositions.Clear();

		for(int x = 1; x < columns - 1; x++)
		{
			for(int y = 1; y < rows - 1; y++)
			{
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
	}

	void BoardSetup()
	{
		boardHolder = new GameObject ("Board").transform;

		for(int x = -1; x < columns + 1; x++)
		{
			for(int y = -1; y < rows + 1; y++)
			{
				GameObject tileToInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

				if(x == -1 || x == columns || y == -1 || y == rows)
				{
					tileToInstantiate = outerwallTiles[Random.Range(0, outerwallTiles.Length)];
				}

				GameObject tile = (GameObject)Instantiate(tileToInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
				tile.transform.SetParent(boardHolder);

			}
		}

        PreviousLevelBoardDestroyer(); // Destroy the previous level board set. So it won't be re-loaded on the next level.
    }

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range(0, gridPositions.Count);
		Vector3 randomPosition = gridPositions[randomIndex];
		gridPositions.RemoveAt(randomIndex);
		return randomPosition;
	}

	void LayoutObjectRandom(GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range(minimum, maximum + 1);

		for(int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoosen = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate(tileChoosen, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level)
	{
		BoardSetup();
		InitializeList();
		LayoutObjectRandom(innerwallTiles, wallCount.minimum, wallCount.maximum);
		LayoutObjectRandom(foodTiles, foodCount.minimum, foodCount.maximum);

		int enemyCount = (int)Mathf.Log(level, 2f);
		LayoutObjectRandom(enemySprites, enemyCount, enemyCount);

		//Instantiate(playerPrefab, new Vector3(columns - columns, rows - rows, 0f), Quaternion.identity);
		Instantiate(exitSign, new Vector3(columns - 1, rows -1, 0f), Quaternion.identity);
	}

    void PreviousLevelBoardDestroyer()
    {
        // Define all game objects by their tag
        string[] tagsToDelete =
        {
            "Wall",
            "Floor",
            "Food",
            "Soda",
            "Exit",
            "Enemy"
        };

        foreach(string tag in tagsToDelete)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(tag);

            if(GameManager.instance.level > 0)
            {
                foreach(GameObject objDel in objectsToDelete)
                {
                    Destroy(objDel);
                }
            }
        }
    }
}
