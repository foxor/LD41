using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

public class NodeList : MonoBehaviour {
	public static NodeList Instance { get; protected set; }

	public void Awake () {
		Instance = this;
	}

	public void Reinitalize() {
		for (int i = transform.childCount - 1; i >= 0; i--) {
			Destroy (transform.GetChild (i).gameObject);
		}

		Type[] nodeTypes = Assembly.GetExecutingAssembly ().GetTypes ()
			.Where (x => !x.IsAbstract && typeof(Node).IsAssignableFrom (x))
			.Where (x => PlayerState.instance.data.UnlockedNodeTypes.Contains (x.Name))
			.OrderBy (x => x.Name)
			.ToArray ();

		GameObject[] nodes = new GameObject[nodeTypes.Length];
		GameObject source = Resources.Load<GameObject> ("NodeView");

		for (int i = 0; i < nodeTypes.Length; i++) {
			nodes [i] = GameObject.Instantiate<GameObject> (source, transform);
			nodes [i].GetComponent<NodeController> ().Setup (nodeTypes [i].Name);
		}

		(transform as RectTransform).sizeDelta = new Vector2 (170f * nodeTypes.Length - 20f, 100f);
	}
}