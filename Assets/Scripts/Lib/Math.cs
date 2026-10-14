using System;
using System.Collections.Generic;
using UnityEngine;

public class MathLib
{
	public static readonly Coordinates[] ORTHOGANAL_DIRECTIONS =
	{
		new Coordinates(0, 1),
		new Coordinates(1, 0),
		new Coordinates(0, -1),
		new Coordinates(-1, 0)
	};
	
	public static List<int> factors(int n)
	{
		List<int> toReturn = new List<int>();
		
		for (int i = 1; i <= n; ++i)
		{
			// Debug.Log("Factoring " + n + " currently at " + i + " % 2 = " + i%2);
			if (n % i == 0)
			{
				toReturn.Add(i);
			}
		}
		
		
		return toReturn;
		
		
	}
	
	public static double ToRadians(double degrees)
	{
		return degrees * Math.PI / 180.0;
	}
	
	public static float Random() {
		return UnityEngine.Random.Range(0.0f, 1.0f);
	}
	
	public static Vector2 CoordinatesBetween(Vector2 oneCorner, Vector2 oppositeCorner)
	{
		return new Vector2(
			UnityEngine.Random.Range(oneCorner.x, oppositeCorner.x),
			UnityEngine.Random.Range(oneCorner.y, oppositeCorner.y)
		);
		
	}
	
}