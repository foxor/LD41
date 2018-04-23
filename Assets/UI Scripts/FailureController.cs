using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureController : MonoBehaviour {
	public static FailureController Instance { get; protected set; }

	public Text reasonText;

	public void Start () {
		Instance = this;
		Close ();
	}

	public void ShowFailurePopup(string reason) {
		reasonText.text = reason;
		gameObject.SetActive (true);
	}

	public void Close() {
		gameObject.SetActive (false);
	}
}