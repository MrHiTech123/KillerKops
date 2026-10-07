using UnityEngine;

public class Protestor : MonoBehaviour
{
	private Coordinates __crowdCoordinates = null;
	private Crowd __crowdBelongsTo;
	
	public Coordinates CrowdCoordinates {get {return __crowdCoordinates;} set {__crowdCoordinates = value;}}
	
	public Crowd CrowdBelongsTo {get {return __crowdBelongsTo;} set {__crowdBelongsTo = value;}}
	
	public void Die()
	{
		CrowdBelongsTo.RemoveProtestor(this.CrowdCoordinates);
	}
	
}