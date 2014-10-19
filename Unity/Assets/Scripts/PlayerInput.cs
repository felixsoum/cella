using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput : MonoBehaviour
{
	public float moveSpeed = 100f;

	private void Update()
	{
		Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		rigidbody2D.velocity = axisInput * moveSpeed * Time.deltaTime;
		BroadcastMessage( "SetFiring", Input.GetButton("Fire1"), SendMessageOptions.DontRequireReceiver  );
	}

	void OnDisable()
	{
		// ghetto game restart
		Application.LoadLevel( Application.loadedLevel );
	}
}
