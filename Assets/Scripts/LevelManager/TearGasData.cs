

using System;
using UnityEngine;

public class TearGasData
{
	public Vector2 position;
	
	public TimeSpan TimeExisted {get; private set;} = TimeSpan.Zero;
	public TimeSpan Duration {get; private set;} = TimeSpan.MaxValue;
	
	public TearGasData(Vector2 position, float duration)
	{
		this.position = position;
		this.Duration = TimeSpan.FromSeconds(duration);
	}
	
}