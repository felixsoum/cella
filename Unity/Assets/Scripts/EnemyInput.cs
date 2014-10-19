using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyInput : MonoBehaviour
{
	public float moveSpeed = 100f;

	public float moveDelayMin = 1.0f;
	public float moveDelayMax = 5.0f;
	
	public float moveDistanceMin = 100.0f;
	public float moveDistanceMax = 100.0f;

	private float moveDelay = 0.0f;
	private float moveElapsed = 0.0f;
	private Vector3 moveInitial;
	private Vector3 moveTarget;

	void Start()
	{

	}

	void Update()
	{
		moveElapsed += Time.deltaTime;
		if( moveElapsed > moveDelay )
		{
			MoveInit();
		}
		MoveUpdate();

		BroadcastMessage( "SetFiring", true, SendMessageOptions.DontRequireReceiver );
		
		if (Common.isOutOfScreen(transform.position))
			Destroy (this.gameObject, 1.0f);
	}

	private void MoveUpdate()
	{
		var moveCurrent = this.transform.position;
		var directionCurrent = (moveTarget - moveCurrent).normalized;
		var directionInitial = (moveTarget - moveInitial).normalized;
		if( directionCurrent.magnitude > 0.0f && Vector2.Dot(directionCurrent, directionInitial) > 0.0f )
		{
			this.rigidbody2D.velocity = moveSpeed * Time.deltaTime * directionCurrent;
		}
		else
		{
			this.rigidbody2D.velocity = Vector3.zero;
		}
	}

	private void ResetMoveDelay()
	{
		moveElapsed = 0.0f;
		moveDelay = Random.Range( moveDelayMin, moveDelayMax );
	}

	private void MoveInit()
	{
		moveInitial = this.transform.position;

		// hard coded because no time
		var targetPoint = new Vector3(2.0f, 0.0f, 0.0f);
		var direction = new Vector3( Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1) ).normalized;
		var displacement = direction * Random.Range(moveDistanceMin, moveDistanceMax);
		moveTarget = targetPoint + displacement;
		ResetMoveDelay();
	}
}
