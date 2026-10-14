using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private float COP_SPAWN_RATE;
	[SerializeField] private float SPECTATOR_SPAWN_RATE;
	
	[SerializeField] private Vector2 COP_SPAWNING_DIMENSIONS_START;
	[SerializeField] private Vector2 COP_SPAWNING_DIMENSIONS_END;
	
	[SerializeField] private Vector2 SPECTATOR_SPAWNING_DIMENSIONS_START;
	[SerializeField] private Vector2 SPECTATOR_SPAWNING_DIMENSIONS_END;
	
	[SerializeField] private GameObject COP_PREFAB;
	[SerializeField] private GameObject SPECTATOR_PREFAB;
	public bool levelStarted = false;
	void SpawnCop()
	{
		Vector2 spawnCoordinates = new Vector2(
			transform.position.x + UnityEngine.Random.Range(COP_SPAWNING_DIMENSIONS_START.x, COP_SPAWNING_DIMENSIONS_END.x),
			transform.position.y + UnityEngine.Random.Range(COP_SPAWNING_DIMENSIONS_START.y, COP_SPAWNING_DIMENSIONS_END.y)
		);
		
		Instantiate(COP_PREFAB, spawnCoordinates, Quaternion.identity);
	}
	
	void UpdateSpawningCop()
	{
		if (MathLib.Random() < COP_SPAWN_RATE * Time.deltaTime)
		{
			SpawnCop();
		}
	}
	void SpawnSpectator()
	{
		Vector2 spawnCoordinates = new Vector2(
			transform.position.x + UnityEngine.Random.Range(SPECTATOR_SPAWNING_DIMENSIONS_START.x, SPECTATOR_SPAWNING_DIMENSIONS_END.x),
			transform.position.y + UnityEngine.Random.Range(SPECTATOR_SPAWNING_DIMENSIONS_START.y, SPECTATOR_SPAWNING_DIMENSIONS_END.y)
		);
		
		Instantiate(SPECTATOR_PREFAB, spawnCoordinates, Quaternion.identity);
	}
	void UpdateSpawningSpectator()
	{
		if (MathLib.Random() < SPECTATOR_SPAWN_RATE * Time.deltaTime)
		{
			SpawnSpectator();
		}
	}
	
	void Update()
	{
		if(levelStarted){
			UpdateSpawningCop();
			UpdateSpawningSpectator();
		}
	}
}