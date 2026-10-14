using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

public class Crowd : MonoBehaviour
{
	
	[SerializeField] private int numPeople;
	
	private static int numPeopleCarriedOver = -1;
	
	[SerializeField] GameObject PROTESTOR_PREFAB;
	
	private int rows;
	private int cols;
	
	private readonly Vector2 DISTANCE_BETWEEN_PROTESTORS = new Vector2(2, 2);
	private readonly float CROWD_MOVEMENT_SPEED = 2f;
	
	List<Protestor> protestors = new List<Protestor>();
	public int getAmountProtesters()
	{
		return protestors.Count;
	}
	Vector2 CorrectProtestorPosition(Coordinates coordinates)
	{
		float startingRowYCoord = transform.position.y - (DISTANCE_BETWEEN_PROTESTORS.y * rows / 2);
		float startingColXCoord = transform.position.x - (DISTANCE_BETWEEN_PROTESTORS.x * cols / 2);
		
		return new Vector2(
			startingColXCoord + (DISTANCE_BETWEEN_PROTESTORS.x * coordinates.col),
			startingRowYCoord + (DISTANCE_BETWEEN_PROTESTORS.y * coordinates.row)
		);
		
	}
	
	TearGasData NearbyTearGas(Protestor protestor)
	{
		TearGasData toReturn = null;
		foreach (TearGasData tearGas in LevelManager.ExistingTearGas)
		{
			if (tearGas.Near(protestor.gameObject))
			{
				if (toReturn == null || toReturn.DistanceTo(protestor.gameObject) > tearGas.DistanceTo(protestor.gameObject))
				{
					toReturn = tearGas;
				}
			}
		}
		return toReturn;
	}
	
	public void OnDestroy()
	{
		numPeopleCarriedOver = protestors.Count;
		if (numPeopleCarriedOver == 1) numPeopleCarriedOver = 2;
	}
	void MoveAllProtestors()
	{
		
		foreach (Protestor protestor in protestors) {
			
			Vector2 correctPosition = CorrectProtestorPosition(protestor.CrowdCoordinates);
			
			TearGasData nearbyGas = NearbyTearGas(protestor);
			if (nearbyGas != null)
			{
				Vector3 distanceFromNearbyGas = protestor.transform.position - (Vector3)nearbyGas.position;
				Vector3 distanceToMoveAway = distanceFromNearbyGas.normalized * TearGasData.DISTANCE_THRESHOLD;
				correctPosition = protestor.transform.position + distanceToMoveAway;
				
			}
			
			if (correctPosition.x > LevelManager.CrowdMovementDimensionsEnd.x)
			{
				correctPosition.x = LevelManager.CrowdMovementDimensionsEnd.x;
			}
			
			if (correctPosition.x < LevelManager.CrowdMovementDimensionsStart.x)
			{
				correctPosition.x = LevelManager.CrowdMovementDimensionsStart.x;
			}
			
			if (correctPosition.y < LevelManager.CrowdMovementDimensionsEnd.y)
			{
				correctPosition.y = LevelManager.CrowdMovementDimensionsEnd.y;
			}
			
			if (correctPosition.y > LevelManager.CrowdMovementDimensionsStart.y)
			{
				correctPosition.y = LevelManager.CrowdMovementDimensionsStart.y;
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
	
	public Coordinates ClosestUnoccupiedCoordinatesNear(Coordinates coordinates, Vector3 startingPosition)
	{
		HashSet<Coordinates> alreadyVisited = new HashSet<Coordinates>();
		PriorityQueue<Coordinates, float> potentialCoordinates = new PriorityQueue<Coordinates, float>();
		alreadyVisited.Add(coordinates);
		potentialCoordinates.Enqueue(coordinates, 0);
		
		
		int count = 0;
		while (potentialCoordinates.Count > 0 && count < 100)
		{
			++count;
			Coordinates candidate = potentialCoordinates.Dequeue();
			
			Debug.Log(candidate.col + "," + candidate.row + " " + count);
			
			if (ProtestorAt(candidate) == null)
			{
				return candidate;
			}
			
			foreach (Coordinates direction in MathLib.ORTHOGANAL_DIRECTIONS)
			{
				Coordinates adjacentCandidate = new Coordinates(coordinates.col + direction.col, coordinates.row + direction.row);
				if (alreadyVisited.Contains(adjacentCandidate))
				{
					continue;
				}
				alreadyVisited.Add(adjacentCandidate);
				potentialCoordinates.Enqueue(adjacentCandidate, (CorrectProtestorPosition(adjacentCandidate) - (Vector2)startingPosition).magnitude);
			}
		}
		return null;
	}
	
	public void CreateProtestorNear(Coordinates coordinates, Vector3 startingPosition)
	{
		Coordinates location = ClosestUnoccupiedCoordinatesNear(coordinates, startingPosition);
		
		if (location == null) {
			location = FirstUnoccupiedLocation();
		}
		if (location == null)
		{
			return;
		}
		CreateProtestor(location, startingPosition);
		
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
		if (numPeopleCarriedOver != -1 && numPeople == -1)
		{
			numPeople = numPeopleCarriedOver;
		}
		
		if (numPeople % 2 == 1) --numPeople;
		
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
