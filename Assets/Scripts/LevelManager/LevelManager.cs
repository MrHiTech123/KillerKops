using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private float COP_SPAWN_RATE;
	[SerializeField] private float SPECTATOR_SPAWN_RATE;
	[SerializeField] private float TEAR_GAS_SPAWN_RATE;
	
	[SerializeField] private Vector2 COP_SPAWNING_DIMENSIONS_START;
	[SerializeField] private Vector2 COP_SPAWNING_DIMENSIONS_END;
	
	[SerializeField] private Vector2 SPECTATOR_SPAWNING_DIMENSIONS_START;
	[SerializeField] private Vector2 SPECTATOR_SPAWNING_DIMENSIONS_END;
	
	[SerializeField] private Vector2 TEAR_GAS_SPAWNING_DIMENSIONS_START;
	[SerializeField] private Vector2 TEAR_GAS_SPAWNING_DIMENSIONS_END;
	[SerializeField] private float TEAR_GAS_DURATION;
	
	
	[SerializeField] private GameObject COP_PREFAB;
	[SerializeField] private GameObject SPECTATOR_PREFAB;
	
	public static readonly List<TearGasData> ExistingTearGas = new List<TearGasData>();

	void Awake()
	{
		ExistingTearGas.Clear();
	}

	public bool levelStarted = false;
	void SpawnCop()
	{
		Vector2 spawnCoordinates = (Vector2)transform.position + MathLib.CoordinatesBetween(COP_SPAWNING_DIMENSIONS_START, COP_SPAWNING_DIMENSIONS_END);
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
		Vector2 spawnCoordinates = (Vector2)transform.position + MathLib.CoordinatesBetween(SPECTATOR_SPAWNING_DIMENSIONS_START, SPECTATOR_SPAWNING_DIMENSIONS_END);
		
		Instantiate(SPECTATOR_PREFAB, spawnCoordinates, Quaternion.identity);
	}
	void UpdateSpawningSpectator()
	{
		if (MathLib.Random() < SPECTATOR_SPAWN_RATE * Time.deltaTime)
		{
			SpawnSpectator();
		}
	}
	
	void SpawnTearGas()
	{
		Vector2 position = (Vector2)transform.position + MathLib.CoordinatesBetween(TEAR_GAS_SPAWNING_DIMENSIONS_START, TEAR_GAS_SPAWNING_DIMENSIONS_END);
		TearGasData gas = new TearGasData(position, TEAR_GAS_DURATION);
		ExistingTearGas.Add(gas);
	}
	void UpdateSpawningTearGas()
	{
		if (MathLib.Random() < TEAR_GAS_SPAWN_RATE * Time.deltaTime)
		{
			SpawnTearGas();
		}
		
		foreach (TearGasData tearGas in ExistingTearGas)
		{
			if (tearGas.TimeExisted > tearGas.Duration)
			{
				ExistingTearGas.Remove(tearGas);
			}
		}
	}
	
	void Update()
	{
		if(levelStarted){
			UpdateSpawningCop();
			UpdateSpawningSpectator();
			UpdateSpawningTearGas();
		}
	}
}