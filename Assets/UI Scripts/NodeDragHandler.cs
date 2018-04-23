using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodeDragHandler : MonoBehaviour
{
	public const float CLICK_GATE_SQR_RADIUS = 10f;
	
	protected Transform customerScrollRect;
	protected Transform dragParent;
	protected Transform finalParent;
	protected Vector3 clickDisplacement;
	protected Vector3 initialClickLocation;

	public Transform toDrag;
	public CanvasGroup canvasGroup;

	private NodeDragHandler dragging;
	private bool copyOnDrag = true;

	public void Start() {
		customerScrollRect = GameObject.FindWithTag ("ScrollRect").transform;
		dragParent = GameObject.FindWithTag ("DragParent").transform;
		finalParent = GameObject.FindWithTag ("NodeParent").transform;
	}

	public void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				if (copyOnDrag) {
					Node intendedCopy = toDrag.GetComponentInParent<NodeController> ().node;
					if (intendedCopy is Always && Always.StartPoint != null) {
						return; // Uh, no threading!
					}

					dragging = Instantiate<GameObject> (toDrag.gameObject).GetComponentInChildren<NodeDragHandler> ();
					dragging.copyOnDrag = false;

					NodeController nodeController = dragging.GetComponentInParent<NodeController> ();
					nodeController.IsInGraph = true;
					SFXPlayer.PlaySound ("PickupNode");
				} else {
					// TODO: box select?
					dragging = this;
				}

				initialClickLocation = Input.mousePosition;
				clickDisplacement = transform.position - initialClickLocation;
				canvasGroup.blocksRaycasts = false;
				dragging.canvasGroup.blocksRaycasts = false;
				dragging.toDrag.SetParent(dragParent);
			}
		}

		if (Input.GetMouseButtonUp(0) && dragging != null) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				canvasGroup.blocksRaycasts = true;
				dragging.canvasGroup.blocksRaycasts = true;

				if (DirectScroll.BlockNodeDrop) {
					Destroy (dragging.toDrag.gameObject);
					SFXPlayer.PlaySound ("DestroyNode");
				} else {
					dragging.toDrag.SetParent (finalParent, true);
					SFXPlayer.PlaySound ("PlaceNode");
				}

				Vector3 totalDisplacement = Input.mousePosition - initialClickLocation;
				if (totalDisplacement.sqrMagnitude < CLICK_GATE_SQR_RADIUS) {
					dragging.GetComponentInParent<NodeController> ().node.OnClicked ();
				}

				dragging = null;
			}
		}

		if (Input.GetMouseButton(0) && dragging != null) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				dragging.toDrag.position = Input.mousePosition + clickDisplacement;
			}
		}
	}
}