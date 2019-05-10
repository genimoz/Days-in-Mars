/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingCharacter
{
	public int enemyDealDamage;

	Animator animator;
	Transform target;
	bool skipMove;

	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList(this);
		animator = GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").transform;

		base.Start();

        enemyDealDamage = Random.Range(10, enemyDealDamage);
    }

	public void MoveEnemy()
	{
		int xDirection = 0;
		int yDirection = 0;

		if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
		{
			yDirection = target.position.y > transform.position.y ? 1 : -1;
		}
		else
		{
			xDirection = target.position.x > transform.position.x ? 1 : -1;
		}

		AttemptToMove<Player>(xDirection, yDirection);
	}

	protected override void AttemptToMove<T>(int xDirection, int yDirection)
	{
		if(skipMove)
		{
			skipMove = false;
			return;
		}

		base.AttemptToMove<T>(xDirection, yDirection);
		skipMove = true;
	}

	protected override void OnCantMove<T>(T Component)
	{
		Player hitPlayer = Component as Player;
		hitPlayer.LoseFood(enemyDealDamage);

		animator.SetTrigger("EnemyAttack");
	}
}
