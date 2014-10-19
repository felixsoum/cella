using UnityEngine;
using System.Collections;

/** 
 * use this for default values 
 */
public class Projectile {
	public Projectile() {
		attack = 1.0f;
	}

	private float attack;
	public float Attack { get {return attack; }  set{ attack = value; } }
}
