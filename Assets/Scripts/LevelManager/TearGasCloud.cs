using System;
using UnityEngine;

public class TearGasCloud : MonoBehaviour
{
	[SerializeField] private Sprite[] sprites;
	
	TimeSpan timeBetweenSprites = TimeSpan.FromSeconds(0.25);
	TimeSpan totalTime = TimeSpan.Zero;

	void Update()
	{
		totalTime += TimeSpan.FromSeconds(Time.deltaTime);
		
		int currentSprite = (int)(totalTime.TotalSeconds / timeBetweenSprites.TotalSeconds);
		
		if (currentSprite >= sprites.Length)
		{
			Destroy(gameObject);
			return;
		}
		
		GetComponent<SpriteRenderer>().sprite = sprites[currentSprite];
		
		
	}


}