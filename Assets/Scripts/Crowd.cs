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
	List<Protestor> protestors = new List<Protestor>();
	
	Vector2 CorrectProtestorPosition(Coordinates coordinates)
	{
		float startingRowYCoord = transform.position.y - (DISTANCE_BETWEEN_PROTESTORS.y * rows / 2);
		float startingColXCoord = transform.position.x - (DISTANCE_BETWEEN_PROTESTORS.x * cols / 2);
		
		return new Vector2(
			startingColXCoord + (DISTANCE_BETWEEN_PROTESTORS.x * coordinates.x),
			startingRowYCoord + (DISTANCE_BETWEEN_PROTESTORS.y * coordinates.y)
		);
		
	}
	void MoveAllProtestors()
	{
		
		foreach (Protestor protestor in protestors) {
			
			Vector2 correctPosition = CorrectProtestorPosition(protestor.CrowdCoordinates);
			
			Debug.Log("Position correct: " + correctPosition.x + ", " + correctPosition.y);
			
			protestor.transform.position = correctPosition;
			
			
		}
		
	}
	
	void CreateProtestor(int row, int col)
	{
		GameObject spawnedActor = Instantiate(PROTESTOR_PREFAB);
		Protestor protestor = spawnedActor.GetComponent<Protestor>();
		
		if (protestor == null) throw new System.Exception("Protestor prefab has no Protestor component");
		
		protestor.CrowdCoordinates = new Coordinates(row, col);
		protestors.Add(protestor);
		
	}
	void SpawnProtestors()
	{
		List<int> factors = Math.factors(numPeople);
		
		if (factors.Count % 2 == 0)
		{
			rows = factors[(factors.Count / 2) - 1];
			cols = factors[factors.Count / 2];
		}
		else
		{
			rows = cols = factors[factors.Count / 2];
		}
		
		for (int r = 0; r < rows; ++r)
		{
			for (int c = 0; c < cols; ++c)
			{
				CreateProtestor(r, c);
				Debug.Log("Creating protestor at (" + r + ", " + c + ")");
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
		// if (Input.GetKey(KeyCode.DownArrow))
		// {
		// 	transform.position += Vector3.down;
		// }
		// if (Input.GetKey(KeyCode.UpArrow))
		// {
		// 	transform.position += Vector3.up;
		// }
		// if (Input.GetKey(KeyCode.LeftArrow))
		// {
		// 	transform.position += Vector3.left;
		// }
		// if (Input.GetKey(KeyCode.RightArrow))
		// {
		// 	transform.position += Vector3.right;
		// }
		
	}
    // Update is called once per frame
    void Update()
    {
		ProcessPlayerInput();
        MoveAllProtestors();
    }
}
