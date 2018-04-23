using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanWorkspace : MonoBehaviour {
	public Transform workspace;
	public float maxZoom = 1.5f;
	public float minZoom = 0.5f;
	public float maxSlope = 1f;

	protected Vector3 clickDisplacement;
	protected Vector3 initialClickLocation;
	protected float aggregatedScroll = 0f;

	public void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				initialClickLocation = Input.mousePosition;
				clickDisplacement = workspace.position - initialClickLocation;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				// do I need this?
			}
		}

		if (Input.GetMouseButton(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				workspace.position = Input.mousePosition + clickDisplacement;
			}
		}
	}
}