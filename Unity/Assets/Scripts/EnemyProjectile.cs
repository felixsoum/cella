using UnityEngine;
using System.Collections;

// todo rename because its used for both enemy and player
public class EnemyProjectile : MonoBehaviour 
{
	public string targetTag = "Player";
	public Vector3 direction = Vector3.left;
	public float speed = 160.0f;

	public GameObject Owner { get; set; }

	// Use this for initialization
	void Start () 
	{
		if( direction.x < 0.0f )
		{
			this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.rigidbody2D.velocity = speed * Time.deltaTime * direction;

		if (Common.isOutOfScreen(transform.position))
			Destroy(this.gameObject);

	}
	

	void OnTriggerEnter2D(Collider2D collision) 
	{
		if( collision.gameObject.tag == targetTag )
		{
			//collision.gameObject.SendMessage("ApplyDamage", stats.Attack, SendMessageOptions.DontRequireReceiver);
			Destroy (this.gameObject);

			// Transfer cell ownership
			var fromComposition = collision.gameObject.GetComponentInParent<CellComposition>();
			var toComposition = Owner != null ? Owner.GetComponentInParent<CellComposition>() : null;
			if( fromComposition != null && toComposition != null )
			{
				var transfered = CellComposition.Transfer( fromComposition, toComposition );

				// Reset some attributes
				transfered.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
				var projComp = transfered.GetComponent<ProjectileFiring>();
				var ownerProj = Owner.GetComponent<ProjectileFiring>();
				if( projComp && ownerProj)
				{
					ownerProj.PasteAttributes( projComp );
				}
				var switchedTag = targetTag == "Enemy" ? "Player" : "Enemy";;
				transfered.gameObject.tag = switchedTag;
				var children = transfered.GetComponentsInChildren<Behaviour>();
				foreach( var c in children )
				{
					c.gameObject.tag = switchedTag;
				}

				// Clean up when composition has no more cells
				if( fromComposition.RemainingCellCount == 0 )
				{
					Destroy( fromComposition.gameObject );
				}
			}
		}
	}


	/* For power stats etc */
	private Projectile stats = new Projectile();
}
