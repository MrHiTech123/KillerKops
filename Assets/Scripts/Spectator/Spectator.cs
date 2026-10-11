using System;
using UnityEngine;

public class Spectator : MonoBehaviour
{
	
	private TimeSpan timeConverted;
	
	void Update()
	{
		
	}
	
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Spectator triggered");
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		
	}
}