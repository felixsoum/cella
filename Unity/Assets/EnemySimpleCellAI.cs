using UnityEngine;
using System.Collections;

public class EnemySimpleCellAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var TP = transform.position;
		pos = Random.Range (0.0f, 8.0f); 
		TP.x = pos - 4.0f;
	}

	void Update() {
		var TP = transform.position;
		pos += Time.deltaTime;
		TP.x = Mathf.PingPong (pos, 8.0f) - 4.0f;
		transform.position = TP;
	}

	private float pos; 
}
