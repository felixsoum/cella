using UnityEngine;
using System.Collections;

public class AnimatorHandling : MonoBehaviour
{
	public void SetFiring( bool firing )
	{
		var animator = GetComponent<Animator>();
		if( animator )
		{
			animator.SetBool("IsFiring", firing);
		}
	}
}
