using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var TP = transform.position;
		TP.x -= 0.1f;
		transform.position = TP;

		if (Common.isOutOfScreen(transform.position))
			Destroy(this.gameObject);

	}
	

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage("ApplyDamage", stats.Attack);
			Destroy (this.gameObject);
		}
	}



	/* For power stats etc */
	private Projectile stats = new Projectile();
}
