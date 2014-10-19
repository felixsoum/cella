using UnityEngine;
using System.Collections;

public class ShootSound : MonoBehaviour
{
	private AudioSource shootAudio;

	private void Start()
	{
		shootAudio = GetComponent<AudioSource>();
	}

	private void PewPew()
	{
		shootAudio.Play();
	}
}
