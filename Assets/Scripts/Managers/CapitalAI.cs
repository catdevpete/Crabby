using UnityEngine;
using System.Collections;

public class CapitalAI : MonoBehaviour
{
	SpawnCrab spawnCrab;

	void Awake()
	{
		spawnCrab = GetComponent<SpawnCrab>();
	}

	void Update()
	{
		spawnCrab.SpawnEnemyCrab(Random.Range(0, 3), 5);
	}
}
