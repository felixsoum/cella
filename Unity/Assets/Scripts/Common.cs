using UnityEngine;

public class Common
{
	/* Don't init me */	
	protected Common ()
	{
	}

	public static bool isOutOfScreen(Vector3 position) {
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(position);

		return screenPosition.y > Screen.height 
			|| screenPosition.y < 0 
			|| screenPosition.x > Screen.width 
			|| screenPosition.x < 0;
	}
}


