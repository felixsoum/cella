using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public Vector3 direction = Vector3.left;
	public float speed = 160.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody2D.velocity = speed * Time.deltaTime * direction;

		if (Common.isOutOfScreen(transform.position))
			Destroy(this.gameObject);

	}
	

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage("ApplyDamage", stats.Attack, SendMessageOptions.DontRequireReceiver);
			Destroy (this.gameObject);
		}
	}



	/* For power stats etc */
	private Projectile stats = new Projectile();
}
