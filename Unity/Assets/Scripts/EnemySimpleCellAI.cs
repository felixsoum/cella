﻿using UnityEngine;
using System.Collections;

/**
 * The basic movement of simple enemies
 */
public class EnemySimpleCellAI : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		Invoke ("TimedShoot", 1);
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
	void TimedShoot() {
		Rigidbody2D p = Instantiate (projectile, transform.position, Quaternion.identity) as Rigidbody2D;
		p.AddForce (Vector2.right * 200);
		Debug.Log ("TEST");
	}
	public Rigidbody2D projectile;
	protected float pos; 
}
