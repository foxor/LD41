using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkDrag : MonoBehaviour {
	protected EdgeView dragging;

	public void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				foreach (GameObject existingOutLink in FindObjectsOfType<EdgeView>().Where(x => x.Root == transform).Select(x => x.gameObject)) {
					Destroy (existingOutLink);
				}
				int index = transform.GetSiblingIndex();
				NodeController nodeController = gameObject.GetComponentInParent<NodeController> ();
				Edge myEdge = nodeController.node.Outputs [index];
				myEdge.OnRight = null;

				Transform wireParent = GameObject.FindWithTag ("LinkParent").transform;
				dragging = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("EdgeView")).GetComponent<EdgeView> ();
				dragging.transform.SetParent (wireParent);
				dragging.Root = transform;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			if (EventSystem.current.currentSelectedGameObject == gameObject) {
				Edge target = LinkDrop.GetDroppedEdge ();
				if (target == null) {
					Destroy (dragging.gameObject);
				} else {
					int index = transform.GetSiblingIndex();
					NodeController nodeController = gameObject.GetComponentInParent<NodeController> ();
					Edge myEdge = nodeController.node.Outputs [index];

					if (myEdge.GetType () != target.GetType ()) {
						SFXPlayer.PlaySound ("LinkFail");
						Destroy (dragging.gameObject);
					} else {
						myEdge.OnRight = target;
						dragging.Target = LinkDrop.CurrentTarget ().transform;
						dragging.IsActive = () => target.IsActive;

						foreach (EdgeView edge in GameObject.FindObjectsOfType<EdgeView>().Where(x => x.Target == dragging.Target)) {
							if (dragging != edge) {
								Destroy (edge.gameObject);
							}
						}

						SFXPlayer.PlaySound ("CreateLink");
					}
				}
			}
		}
	}
}