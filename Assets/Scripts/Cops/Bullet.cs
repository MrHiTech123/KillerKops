using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public static readonly float BULLET_SPEED = 10;
	void Start()
	{
		
	}
	
	
	void HandleMovement()
	{
		float angle = transform.rotation.z;
		
		float xMult = (float)Math.Sin(angle);
		float yMult = (float)Math.Cos(angle);
		
		Vector2 movementDirection = Time.deltaTime * BULLET_SPEED * new Vector2(xMult, yMult);
		
		transform.position += (Vector3)movementDirection;
		
	}
	
	void Update()
	{
		HandleMovement();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Colliding");
		GameObject other = collision.gameObject;
		
		if (UnityLib.HasComponent<Protestor>(other))
		{
			Protestor protestor = other.GetComponent<Protestor>();
			protestor.Die();
		}
		else
		{
			Debug.Log("No Protestor");
		}
		
	}
}