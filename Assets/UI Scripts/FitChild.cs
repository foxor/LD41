using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitChild : MonoBehaviour {

	public Vector2 Padding;

	void Update () {
		Vector2 childSizeDelta = (transform.GetChild (0) as RectTransform).sizeDelta;
		(transform as RectTransform).sizeDelta = childSizeDelta + Padding * 2;
	}
}
