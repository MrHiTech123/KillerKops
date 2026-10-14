

using System;
using UnityEngine;

public class TearGasData
{
	public static readonly float DISTANCE_THRESHOLD = 3;
	public Vector2 position;
	
	public TimeSpan TimeExisted = TimeSpan.Zero;
	public TimeSpan Duration {get; private set;} = TimeSpan.MaxValue;
	
	public TearGasData(Vector2 position, float duration)
	{
		this.position = position;
		this.Duration = TimeSpan.FromSeconds(duration);
	}
	
	public float DistanceTo(GameObject gameObject)
	{
		return ((Vector2)gameObject.transform.position - this.position).magnitude;
	}
	public bool Near(GameObject gameObject)
	{
		
		if (DistanceTo(gameObject) < DISTANCE_THRESHOLD)
		{
			return true;
		}
		
		return false;
	}
	
}