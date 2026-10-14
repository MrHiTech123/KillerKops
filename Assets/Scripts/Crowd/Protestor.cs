using System;
using UnityEngine;

public class Protestor : MonoBehaviour
{
	private Coordinates __crowdCoordinates = null;
	private Crowd __crowdBelongsTo;
	
	public Coordinates CrowdCoordinates {get {return __crowdCoordinates;} set {__crowdCoordinates = value;}}
	
	public Crowd Crowd {get {return __crowdBelongsTo;} set {__crowdBelongsTo = value;}}
	
	private TimeSpan TimeSinceLastShot = TimeSpan.Zero;
	
	[SerializeField] private float __secondsBetweenShots;
	private TimeSpan TimeBetweenShots;
	public GameObject BULLET_PREFAB;
	
	[SerializeField] private Sprite SIGN_PROTESTOR;
	[SerializeField] private Sprite GUN_PROTESTOR;

	void Awake()
	{
		TimeBetweenShots = TimeSpan.FromSeconds(__secondsBetweenShots);
		TimeSinceLastShot = TimeSpan.FromSeconds(UnityEngine.Random.Range(0, __secondsBetweenShots));
	}

	void Start()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		if (LevelManager.ProtestorsHaveGuns)
		{
			renderer.sprite = GUN_PROTESTOR;
		}
		else
		{
			renderer.sprite = SIGN_PROTESTOR;
		}
	}
	public void Die()
	{
		Crowd.RemoveProtestor(this.CrowdCoordinates);
		Destroy(this.gameObject);
	}
	
	Quaternion RotationFromDirection()
	{
		return Quaternion.Euler(0, 0, 90);
	}
	void ShootBullet()
	{
		GameObject bullet = Instantiate(BULLET_PREFAB, transform.position, RotationFromDirection());
		
		Bullet bulletComponent = bullet.GetComponent<Bullet>();
		bulletComponent.targettingMode = BulletTargettingMode.COP;
		
		TimeSinceLastShot = TimeSpan.FromSeconds(0);
		SoundManager.Instance.GunShot(transform.position);
	}
	
	void HandleShooting()
	{
		Debug.Log("Time since last " + TimeSinceLastShot.TotalSeconds);
		TimeSinceLastShot += TimeSpan.FromSeconds(Time.deltaTime);
		
		if (TimeSinceLastShot > TimeBetweenShots)
		{
			ShootBullet();
		}
		
	}

	public void UpdateGun()
	{
		if (LevelManager.ProtestorsHaveGuns)
		{
			HandleShooting();
		}
	}
	public void Update()
	{
		UpdateGun();
	}

}