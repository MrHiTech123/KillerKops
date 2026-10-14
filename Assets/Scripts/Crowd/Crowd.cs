using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Crowd : MonoBehaviour
{
	
	[SerializeField] private int numPeople;
	
	[SerializeField] GameObject PROTESTOR_PREFAB;
	
	private int rows;
	private int cols;
	
	private readonly Vector2 DISTANCE_BETWEEN_PROTESTORS = new Vector2(2, 2);
	private readonly float CROWD_MOVEMENT_SPEED = 1f;
	
	List<Protestor> protestors = new List<Protestor>();
	
	Vector2 CorrectProtestorPosition(Coordinates coordinates)
	{
		float startingRowYCoord = transform.position.y - (DISTANCE_BETWEEN_PROTESTORS.y * rows / 2);
		float startingColXCoord = transform.position.x - (DISTANCE_BETWEEN_PROTESTORS.x * cols / 2);
		
		return new Vector2(
			startingColXCoord + (DISTANCE_BETWEEN_PROTESTORS.x * coordinates.col),
			startingRowYCoord + (DISTANCE_BETWEEN_PROTESTORS.y * coordinates.row)
		);
		
	}
	
	TearGasData NearbyTearGas()
	{
		TearGasData toReturn = null;
		foreach (TearGasData tearGas in LevelManager.ExistingTearGas)
		{
			if (tearGas.Near(gameObject))
			{
				if (toReturn == null || toReturn.DistanceTo(gameObject) > tearGas.DistanceTo(gameObject))
				{
					toReturn = tearGas;
				}
			}
		}
		return toReturn;
	}
	void MoveAllProtestors()
	{
		
		foreach (Protestor protestor in protestors) {
			
			Vector2 correctPosition = CorrectProtestorPosition(protestor.CrowdCoordinates);
			
			TearGasData nearbyGas = NearbyTearGas();
			if (nearbyGas != null)
			{
				Vector3 distanceFromNearbyGas = transform.position - (Vector3)nearbyGas.position;
				Vector3 distanceToMoveAway = distanceFromNearbyGas.normalized * TearGasData.DISTANCE_THRESHOLD;
				correctPosition = transform.position + distanceToMoveAway;
				
			}
			
			UnityLib.MoveAtSpeed(protestor.gameObject, CROWD_MOVEMENT_SPEED, correctPosition);
			
			
		}
		
	}
	
	public void CreateProtestor(Coordinates coordinates, Vector3 startingPosition)
	{
		Debug.Log("Starting at " + startingPosition + " ending at " + CorrectProtestorPosition(coordinates));
		GameObject spawnedActor = Instantiate(PROTESTOR_PREFAB, startingPosition, Quaternion.identity);
		Protestor protestor = spawnedActor.GetComponent<Protestor>();
		
		if (protestor == null) throw new System.Exception("Protestor prefab has no Protestor component");
		
		protestor.CrowdCoordinates = coordinates;
		protestor.Crowd = this;
		protestors.Add(protestor);
		
	}
	
	public void CreateProtestor(Coordinates coordinates)
	{
		Vector3 startingPosition = CorrectProtestorPosition(coordinates);
		CreateProtestor(coordinates, startingPosition);
	}
	
	public void CreateProtestorNear(Coordinates coordinates, Vector3 startingPosition)
	{
		foreach (Coordinates direction in MathLib.ORTHOGANAL_DIRECTIONS)
		{
			Coordinates potentialCoordinates = new Coordinates(coordinates.col + direction.col, coordinates.row + direction.row);
			if (ProtestorAt(potentialCoordinates) == null)
			{
				CreateProtestor(potentialCoordinates, startingPosition);
				return;
			}
		}
		
		Coordinates location = FirstUnoccupiedLocation();
		if (location == null)
		{
			return;
		}
		CreateProtestor(location);
		
	}
	
	public Coordinates FirstUnoccupiedLocation()
	{
		for (int col = 0; col < cols; ++col)
		{
			for (int row = 0; row < rows; ++row)
			{
				Coordinates toReturnCandidate = new Coordinates(col, row);
				if (ProtestorAt(toReturnCandidate) == null)
				{
					return toReturnCandidate;
				}
			}
		}
		return null;
	}
	
	public Protestor ProtestorAt(Coordinates coordinates)
	{
		foreach (Protestor protestor in protestors)
		{
			if (protestor.CrowdCoordinates.Equals(coordinates))
			{
				return protestor;
			}
		}
		return null;
	}
	
	public void RemoveProtestor(Coordinates coordinatesToRemove)
	{
		Protestor toRemove = ProtestorAt(coordinatesToRemove);
		if (toRemove == null)
		{
			return;
		}
		protestors.Remove(toRemove);
	}
	void SpawnProtestors()
	{
		List<int> factors = MathLib.factors(numPeople);
		
		if (factors.Count % 2 == 0)
		{
			cols = factors[(factors.Count / 2) - 1];
			rows = factors[factors.Count / 2];
		}
		else
		{
			rows = cols = factors[factors.Count / 2];
		}
		
		for (int r = 0; r < rows; ++r)
		{
			for (int c = 0; c < cols; ++c)
			{
				CreateProtestor(new Coordinates(c, r));
			}
		}
		
		MoveAllProtestors();
		
		
	}
	void Awake()
	{
		
	}


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        SpawnProtestors();
    }
	
	
	void ProcessPlayerInput()
	{
		Vector3 toMove = new Vector3(0, 0, 0);
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			toMove += Vector3.down;
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			toMove += Vector3.up;
		}
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			toMove += Vector3.left;
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			toMove += Vector3.right;
		}
		
		toMove = toMove.normalized * CROWD_MOVEMENT_SPEED * Time.deltaTime;
		transform.position += toMove;
		
		
	}
    // Update is called once per frame
    void Update()
    {
		ProcessPlayerInput();
        MoveAllProtestors();
    }
}
