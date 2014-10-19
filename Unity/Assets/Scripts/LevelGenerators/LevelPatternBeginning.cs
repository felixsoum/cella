using UnityEngine;

/**
 * The level spawn pattern for the first level
 */
public class LevelPatternBeginning : MonoBehaviour
{
	/* First basic enemy */
	public Rigidbody2D bloodCell; 
	public Rigidbody2D EColi;

	void Start() {
		run ();
	}

	/* Enemies in first wave will first have a */
	public void run() {
		int seconds = 0;
		enemyWave ();
	}


	private void enemyWave() {
		var xpos = 4;
		var ypos = -1.0f; 
		var loopCount = 5f; 

		for (float z = 0f; z < loopCount; z += 1f) {
			var clone = Instantiate(bloodCell, new Vector2(xpos, ypos + ((z / loopCount) * 2.0f)), Quaternion.identity);

		}
	}
}


