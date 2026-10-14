using System;
using UnityEngine;


public enum BulletTargettingMode
{
	PROTESTOR,
	COP
}
public class Bullet : MonoBehaviour
{
	public BulletTargettingMode targettingMode = BulletTargettingMode.PROTESTOR;
	
	public static readonly float BULLET_SPEED = 10;
	void Start()
	{
		
	}
	
	
	void HandleMovement()
	{
		float angle = transform.localEulerAngles.z;
		
		// Debug.Log(angle + " rad " + MathLib.ToRadians(angle));
		
		float xMult = (float)Math.Cos(MathLib.ToRadians(angle));
		float yMult = (float)Math.Sin(MathLib.ToRadians(angle));
		
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
		
		if (targettingMode == BulletTargettingMode.PROTESTOR && UnityLib.HasComponent<Protestor>(other))
		{
			Protestor protestor = other.GetComponent<Protestor>();
			protestor.Die();
			Destroy(this.gameObject);
		}
		if (targettingMode == BulletTargettingMode.COP && UnityLib.HasComponent<Cop>(other))
		{
			Cop cop = other.GetComponent<Cop>();
			cop.Die();
			Destroy(this.gameObject);
		}
		else
		{
			Debug.Log("No Protestor");
		}
		
	}
}