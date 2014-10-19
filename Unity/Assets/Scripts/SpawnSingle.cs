using UnityEngine;
using System.Collections;

public class SpawnSingle : MonoBehaviour
{
	public GameObject enemy;

	private void Spawn()
	{
		Instantiate(enemy, transform.position, Quaternion.identity);
	}
}
