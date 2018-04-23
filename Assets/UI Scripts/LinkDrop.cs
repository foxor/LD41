using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	protected static List<LinkDrop> activeMouseovers = new List<LinkDrop> ();

	public static LinkDrop CurrentTarget() {
		return activeMouseovers.Single ();
	}

	public static Edge GetDroppedEdge() {
		if (activeMouseovers.Count != 1) {
			return null;
		}
		LinkDrop dropped = activeMouseovers.Single ();
		int index = dropped.transform.GetSiblingIndex();
		NodeController nodeController = dropped.gameObject.GetComponentInParent<NodeController> ();
		return nodeController.node.Inputs [index];
	}

	public void OnPointerEnter(PointerEventData eventData) {
		activeMouseovers.Add (this);
	}

	public void OnPointerExit(PointerEventData eventData) {
		activeMouseovers.Remove (this);
	}
}