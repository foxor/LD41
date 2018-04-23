using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeView : MonoBehaviour {
	public Transform Root;
	public Transform Target;

	public Image lineImage;

	public Func<bool> IsActive = () => false;

	protected RectTransform myRect;
	protected bool hasBeenDropped = false;

	public void Start() {
		myRect = transform as RectTransform;
	}

	public void LateUpdate () {
		if ((hasBeenDropped && Target == null) || Root == null) {
			Destroy (gameObject);
			return;
		}

		lineImage.color = IsActive () ? ColorManager.Instance.blueActive : ColorManager.Instance.blueForeground;

		myRect.position = Root.position;
		hasBeenDropped |= Target != null;
		Vector3 targetPosition = hasBeenDropped ? Target.position : Input.mousePosition;
		Vector3 displacement = targetPosition - transform.position;
		myRect.sizeDelta = new Vector2 (displacement.magnitude, 3f);
		myRect.rotation = Quaternion.Euler (0f, 0f, Mathf.Atan2 (displacement.y, displacement.x) * Mathf.Rad2Deg);
	}
}
