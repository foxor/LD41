using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour {
	public string NodeTypeName;
	public Node node { get; protected set; }

	public Text Name;
	public Transform InputParent;
	public Transform OutputParent;
	public bool IsInGraph = false;

	public Image Background;

	protected Text[] inputEdgeEndpoints = new Text[0];
	protected Text[] outputEdgeEndpoints = new Text[0];

	protected Text[] SetupNodes(GameObject resource, Transform parent, Edge[] sources) {
		for (int i = parent.childCount - 1; i >= 0; i--) {
			Destroy (parent.GetChild (i).gameObject);
		}

		Text[] endpoints = new Text[sources.Length];

		for (int i = 0; i < endpoints.Length; i++) {
			endpoints [i] = GameObject.Instantiate<GameObject> (resource, parent).GetComponent<Text>();
		}

		return endpoints;
	}

	public void Start() {
		if (!string.IsNullOrEmpty (NodeTypeName)) {
			Setup (NodeTypeName);
		}
	}

	public void OnDestroy() {
		if (Always.StartPoint == node) {
			Always.StartPoint = null;
		}
		node = null;
	}

	public void Setup(string nodeTypeName) {
		NodeTypeName = nodeTypeName;
		Type nodeType = Assembly.GetExecutingAssembly ().GetType (nodeTypeName);
		node = (Node)System.Activator.CreateInstance (nodeType);

		// These are special, only if they are actually in user code, since that's the app start point
		if (node is Always && IsInGraph) {
			(node as Always).RegisterTick ();
		}
		if (node is StartNode && IsInGraph) {
			(node as StartNode).RegisterTick ();
		}

		inputEdgeEndpoints = SetupNodes (Resources.Load<GameObject>("InputEdge"), InputParent, node.Inputs);
		outputEdgeEndpoints = SetupNodes (Resources.Load<GameObject>("OutputEdge"), OutputParent, node.Outputs);
	}

	protected void UpdateEndpoints(Text[] endpoints, Edge[] edges) {
		for (int i = 0; i < endpoints.Length; i++) {
			endpoints [i].text = edges [i].Symbol;
		}
	}

	public void Update () {
		if (node != null) {
			name = node.Name ();
			Name.text = node.Name ();

			UpdateEndpoints (inputEdgeEndpoints, node.Inputs);
			UpdateEndpoints (outputEdgeEndpoints, node.Outputs);

			Color AppropriateColor = node.IsActive ? ColorManager.Instance.blueActive : ColorManager.Instance.blueForeground;

			Background.color = AppropriateColor;
			foreach (Text text in inputEdgeEndpoints.Where(x => x != null)) {
				text.color = AppropriateColor;
			}
			foreach (Text text in outputEdgeEndpoints.Where(x => x != null)) {
				text.color = AppropriateColor;
			}
			Name.color = AppropriateColor;
		}
	}
}