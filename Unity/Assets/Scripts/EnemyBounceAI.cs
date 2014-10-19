
using UnityEngine;
using System.Collections;

public class EnemyBounceAI : EnemySimpleCellAI
{
	
	public EnemyBounceAI ()
	{
	}

	void Update() {
		float theTime = (float)Time.deltaTime;
		var TP = transform.position;
		pos += Time.deltaTime;
		TP.x -= 0.03f;
		TP.y = 2.0f * Mathf.PingPong (pos, 2.0f);
		transform.position = TP;
	}


}


