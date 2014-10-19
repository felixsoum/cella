using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float moveSpeed = 100f;
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		rigidbody2D.velocity = axisInput * moveSpeed * Time.deltaTime;
		animator.SetBool("IsFiring", Input.GetButton("Fire1"));
	}

	private void ApplyDamage(float receive) {
		// TODO
	}


}
