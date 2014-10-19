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
		int seconds = 1;
		Invoke ("EnemyWave", 1);
		Invoke ("EnemyWaveHarder", seconds += 10);
		Invoke ("EnemyWave", seconds += 10);
		Invoke ("EnemyWave", seconds += 10);
		Invoke ("EnemyWaveHarder", seconds += 10);
		Invoke ("EnemyWave", seconds += 10);
	}


	private void EnemyWave() {
		var xpos = 4;
		var ypos = -1.0f; 
		var loopCount = 5f; 

		for (float z = 0f; z < loopCount; z += 1f) {
			var clone = Instantiate(
				bloodCell, 
				new Vector2(xpos, ypos + ((z / loopCount) * 2.0f)), 
				Quaternion.identity);
		}
	}

	private void EnemyWaveHarder() {
		var xpos = 4;
		var ypos = -1.0f; 
		var loopCount = 10f; 
		
		for (float z = 0f; z < loopCount; z += 1f) {
			var clone = Instantiate(
				bloodCell, 
				new Vector2(xpos, ypos + ((z / loopCount) * 2.0f)), 
				Quaternion.identity);

			/* zig zag */
			z += 1f;
			var clone2 = Instantiate(
				bloodCell, 
				new Vector2(xpos - 1, ypos + ((z / loopCount) * 2.0f)), 
				Quaternion.identity);
		}
	}
}


