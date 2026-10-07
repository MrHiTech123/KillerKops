using UnityEngine;

public class Protestor : MonoBehaviour
{
	private Coordinates __crowdCoordinates = null;
	
	public Coordinates CrowdCoordinates {get {return __crowdCoordinates;} set {__crowdCoordinates = value;}}
	
}