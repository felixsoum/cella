using UnityEngine;
using System.Collections;

public class SpawnTimer : MonoBehaviour
{
	public float timeBeforeActivation = 0;
	public float timeBetweenSpawning = 2f;

	private void Start()
	{
		Invoke("BroadcastSpawn", timeBeforeActivation);
	}

	private void BroadcastSpawn()
	{
		BroadcastMessage("Spawn");
		Invoke("BroadcastSpawn", timeBetweenSpawning);
	}
}
