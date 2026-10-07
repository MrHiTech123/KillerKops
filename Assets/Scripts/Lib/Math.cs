using System.Collections.Generic;
using UnityEngine;

public class MathLib
{
	
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
	
	
}