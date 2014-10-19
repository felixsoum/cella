using UnityEngine;
using System.Collections;

public class ProjectileFiring : MonoBehaviour
{
	[Tooltip("Specify the prefab used to instantiate each projectile")]
	public GameObject projectilePrefab;
	public string projectileTargetTag;

	[Tooltip("Specify the projectile movement velocity in unity units per second")]
	public Vector3 velocity = new Vector3(160.0f, 0.0f, 0.0f);
	[Tooltip("Specify the delay in seconds between each projectile fired")]
	public float delay = 0.2f;

	public bool IsFiring { get; set; }

	private float elapsedFire = 0.0f;

	private void Update()
	{
		elapsedFire += Time.deltaTime;
		if( IsFiring && elapsedFire > delay )
		{
			Fire();
			elapsedFire = 0.0f;
		}
	}

	public void SetFiring( bool firing )
	{
		IsFiring = firing;
	}

	private void Fire()
	{
		var projectile = Instantiate( projectilePrefab ) as GameObject;
		projectile.transform.position = this.transform.position;

		var projComp = projectile.GetComponent<EnemyProjectile>();
		if( projComp != null )
		{
			projComp.speed = velocity.magnitude;
			projComp.direction = velocity.normalized;
			projComp.targetTag = projectileTargetTag;
		}
	}
}
