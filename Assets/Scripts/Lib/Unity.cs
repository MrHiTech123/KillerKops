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
	
	void MoveAtSpeed(GameObject gameObject, float distanceToTravel, Vector3 destination)
	{
		Vector3 distanceToDestination = destination - gameObject.transform.position;
		
		if (distanceToDestination.magnitude < distanceToTravel)
		{
			gameObject.transform.position = destination;
		}
		
		else
		{
			Vector3 toTravel = distanceToDestination.normalized * distanceToTravel;
			gameObject.transform.position += toTravel;
		}
		
	}
}