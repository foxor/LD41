using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
	public static SFXPlayer Instance { get; protected set; }

	public float lowPitchRange = .75F;
	public float highPitchRange = 1.5F;

	public AudioSource source;

	public void Awake() {
		Instance = this;
	}

	public static void PlaySound(string soundName) {
		AudioClip clip = Resources.Load<AudioClip> (soundName);
		Instance.source.pitch = Random.Range (Instance.lowPitchRange, Instance.highPitchRange);
		Instance.source.PlayOneShot (clip);
	}
}