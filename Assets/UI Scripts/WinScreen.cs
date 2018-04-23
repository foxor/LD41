using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {
	public static WinScreen Instance { get; protected set; }

	public void Awake () {
		Instance = this;
		gameObject.SetActive (false);
	}
}