using UnityEngine;
using System.Collections;

public class CommieAI : MonoBehaviour
{
	SpawnCrab spawnCrab;

	void Awake()
	{
		spawnCrab = GetComponent<SpawnCrab>();
	}

	void Update()
	{
		spawnCrab.SpawnPlayerCrab(Random.Range(0, 3), 5);
	}
}
