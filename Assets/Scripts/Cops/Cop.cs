using System;
using UnityEngine;

public class Cop : MonoBehaviour
{
	[SerializeField] private float __secondsBetweenShots;
	[SerializeField] private GameObject BULLET_PREFAB;
	
	private TimeSpan TimeBetweenShots;
	private TimeSpan TimeSinceLastShot;
	void Awake()
	{
		TimeBetweenShots = TimeSpan.FromSeconds(__secondsBetweenShots);
		TimeSinceLastShot = TimeSpan.FromSeconds(UnityEngine.Random.Range(0, __secondsBetweenShots));
	}
	
	void ShootBullet()
	{
		GameObject bullet = Instantiate(BULLET_PREFAB, transform.position, transform.rotation);
		TimeSinceLastShot = TimeSpan.FromSeconds(0);
	}
	
	void HandleShooting()
	{
		TimeSinceLastShot += TimeSpan.FromSeconds(Time.deltaTime);
		
		if (TimeSinceLastShot > TimeBetweenShots)
		{
			ShootBullet();
		}
		
	}
	void Update()
	{
		HandleShooting();
	}
}