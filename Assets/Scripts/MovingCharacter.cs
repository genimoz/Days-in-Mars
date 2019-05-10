/*
* Author : Genimoz
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using UnityEngine;

public abstract class MovingCharacter : MonoBehaviour
{
	public float moveTime = 0.1f;
	public LayerMask blockingLayer;

	private BoxCollider2D boxCol;
	private Rigidbody2D rb2d;
	private float inversMoveTime;

	protected virtual void Start ()
	{
		boxCol = GetComponent<BoxCollider2D>();
		rb2d = GetComponent<Rigidbody2D>();
		inversMoveTime = 1f / moveTime;
	}

	protected bool Move(int xDirection, int yDirection, out RaycastHit2D hit)
	{
		Vector2 startPosition = transform.position;
		Vector2 endPosition = startPosition + new Vector2(xDirection, yDirection);

		boxCol.enabled = false;

		hit = Physics2D.Linecast(startPosition, endPosition, blockingLayer);

		boxCol.enabled = true;

		if(hit.transform == null)
		{
			StartCoroutine(SmoothMovement(endPosition));
			return true;
		}

		return false;
	}

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float squareRemainingDistance = (transform.position - end).sqrMagnitude;

		while(squareRemainingDistance > float.Epsilon)
		{
			Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inversMoveTime * Time.deltaTime);
			rb2d.MovePosition(newPosition);
			squareRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AttemptToMove<T>(int xDirection, int yDirection)
		where T : Component
	{
		RaycastHit2D hit;
		bool canMove = Move(xDirection, yDirection, out hit);

		if(hit.transform == null)
		{
			return;
		}

		T hitComponent = hit.transform.GetComponent<T>();

		if(!canMove && hitComponent != null)
		{
			OnCantMove(hitComponent);
		}
	}

	protected abstract void OnCantMove<T>(T Component)
		where T : Component;
}
