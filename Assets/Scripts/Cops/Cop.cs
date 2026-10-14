using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class Cop : MonoBehaviour
{
	public float __secondsBetweenShots;
	[SerializeField] private GameObject BULLET_PREFAB;
	
	private static readonly float MOVEMENT_SPEED = 1;
	
	private TimeSpan TimeBetweenShots;
	private TimeSpan TimeSinceLastShot;
	
	
	public float __minTimeBetweenMovementChanges;
	public float __maxTimeBetweenMovementChanges;
	
	private TimeSpan MIN_TIME_BETWEEN_MOVEMENT_CHANGES;
	private TimeSpan MAX_TIME_BETWEEN_MOVEMENT_CHANGES;
	private TimeSpan TimeOfNextMovementChange;
	private TimeSpan TimeSinceLastMovementChange;
	
	private CopMovementDirection movementDirection;
	
	void ResetMovemementChange()
	{
		
		TimeSinceLastMovementChange = TimeSpan.FromSeconds(0);
		TimeOfNextMovementChange = TimeSpan.FromSeconds(UnityEngine.Random.Range(
			(float)MIN_TIME_BETWEEN_MOVEMENT_CHANGES.TotalSeconds,
			(float)MAX_TIME_BETWEEN_MOVEMENT_CHANGES.TotalSeconds)
		);
	}
	void Awake()
	{
		TimeBetweenShots = TimeSpan.FromSeconds(__secondsBetweenShots);
		TimeSinceLastShot = TimeSpan.FromSeconds(UnityEngine.Random.Range(0, __secondsBetweenShots));
		
		MIN_TIME_BETWEEN_MOVEMENT_CHANGES = TimeSpan.FromSeconds(__minTimeBetweenMovementChanges);
		MAX_TIME_BETWEEN_MOVEMENT_CHANGES = TimeSpan.FromSeconds(__maxTimeBetweenMovementChanges);
		
		ResetMovemementChange();
	}
	
	public void Die()
	{
		Destroy(this.gameObject);
	}

    
	Quaternion RotationFromDirection()
	{
		return Quaternion.Euler(0, 0, -90);
	}
    void ShootBullet()
	{
		GameObject bullet = Instantiate(BULLET_PREFAB, transform.position, RotationFromDirection());
		TimeSinceLastShot = TimeSpan.FromSeconds(0);
		SoundManager.Instance.GunShot(transform.position);
	}
	
	void HandleShooting()
	{
		if (!LevelManager.CopsCanShoot) return;
		TimeSinceLastShot += TimeSpan.FromSeconds(Time.deltaTime);
		
		if (TimeSinceLastShot > TimeBetweenShots)
		{
			ShootBullet();
		}
		
	}
	
	void SwitchDirections()
	{
		Debug.Log("Switching directions from " + movementDirection);
		if (movementDirection == CopMovementDirection.LEFT)
		{
			movementDirection = CopMovementDirection.RIGHT;
		}
		else
		{
			movementDirection = CopMovementDirection.LEFT;
		}
		// movementDirection = (movementDirection == CopMovementDirection.LEFT)? CopMovementDirection.RIGHT : CopMovementDirection.LEFT;
		
	}
	
	void HandleMovement()
	{
		TimeSinceLastMovementChange += TimeSpan.FromSeconds(Time.deltaTime);
		if (TimeSinceLastMovementChange > TimeOfNextMovementChange)
		{
			SwitchDirections();
			ResetMovemementChange();
		}
		
		int movementSign = (movementDirection == CopMovementDirection.LEFT)? -1 : 1;
		
		float amountToMove = Time.deltaTime * movementSign * MOVEMENT_SPEED;
		
		transform.position += new Vector3(amountToMove, 0, 0);
		
	}
	
	void Update()
	{
		HandleShooting();
		HandleMovement();
	}
}