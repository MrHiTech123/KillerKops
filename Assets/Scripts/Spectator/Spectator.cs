using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spectator : MonoBehaviour
{
	
	private TimeSpan timeSpentBeingConverted;
	private static readonly TimeSpan CONVERSION_TIME = TimeSpan.FromSeconds(1);
	
	SpriteRenderer renderer;
	private HashSet<GameObject> currentlyCollidedObjects = new HashSet<GameObject>();

	void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
	}
	Protestor TouchedProtestor()
	{
		foreach (GameObject gameObject in currentlyCollidedObjects)
		{
			if (UnityLib.HasComponent<Protestor>(gameObject))
			{
				return gameObject.GetComponent<Protestor>();
			}
		}
		return null;
	}
	bool TouchingProtestor()
	{
		if (TouchedProtestor() == null)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	void BeConvertedBy(Protestor converter)
	{
		converter.Crowd.CreateProtestorNear(converter.CrowdCoordinates, transform.position);
		Destroy(gameObject);
	}
	
	double ConversionLerpValue()
	{
		return timeSpentBeingConverted.TotalSeconds / CONVERSION_TIME.TotalSeconds;
	}
	void UpdateConversionColor()
	{
		renderer.color = Color.Lerp(Color.white, Color.red, (float)ConversionLerpValue());
		Debug.Log(renderer.color);
	}
	
	void UpdateConversion()
	{
		if (TouchingProtestor())
		{
			timeSpentBeingConverted += UnityLib.DeltaTimeSpan();
		}
		else
		{
			timeSpentBeingConverted -= UnityLib.DeltaTimeSpan() / 2;
		}
		
		if (timeSpentBeingConverted < TimeSpan.Zero)
		{
			timeSpentBeingConverted = TimeSpan.Zero;
		}
		
		if (timeSpentBeingConverted >= CONVERSION_TIME)
		{
			BeConvertedBy(TouchedProtestor());
		}
		
		UpdateConversionColor();
		
	}
	
	void Update()
	{
		UpdateConversion();
	}
	
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		currentlyCollidedObjects.Add(collision.gameObject);
		Debug.Log("Spectator triggered");
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		currentlyCollidedObjects.Remove(collision.gameObject);
	}
}