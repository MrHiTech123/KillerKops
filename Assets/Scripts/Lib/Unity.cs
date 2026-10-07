using UnityEngine;

public class UnityLib
{
	public static bool HasComponent<T>(GameObject inputObject)
	{
		T component = inputObject.GetComponent<T>();
		if (component == null)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}