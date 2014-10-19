using UnityEngine;
using System.Collections;

/**
 * The basic movement of simple enemies
 */
public class EnemySimpleCellAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var TP = transform.position;
		pos = Random.Range (0.0f, 8.0f); 
		TP.x = pos - 4.0f;
	}
	
	void Update() {
		float theTime = (float)Time.deltaTime;
		var TP = transform.position;
		pos += Time.deltaTime;
		TP.x -= 0.03f;
		transform.position = TP;
	}

	protected float pos; 
}
