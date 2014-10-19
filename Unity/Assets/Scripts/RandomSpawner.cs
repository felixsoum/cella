using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
	public void Start()
	{
		Move();
	}

	private void Move()
	{
		transform.position = new Vector2(transform.position.x, Random.Range(-1.6f, 1.6f));
		Invoke("Move", 0.5f);
	}
}
