using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Edge {
	public bool IsActive { get; protected set; }
	public abstract string Symbol { get; }

	protected Node Pair;
	public Edge OnRight { protected get; set; }

	public object value { get; protected set; }

	public Edge(Node Pair) {
		this.Pair = Pair;
	}

	public IEnumerator Activate(object value = null, bool leftSide = true) {
		this.value = value;
		IsActive = true;

		if (leftSide) {
			if (OnRight != null) {
				yield return OnRight.Activate (value, false);
			}
		} else {
			yield return Pair.Activate ();
		}

		IsActive = false;
		this.value = null;
	}
}

public abstract class DataEdge<T> : Edge {
	public T Value {
		get {
			if (value == null) {
				return default(T);
			}
			return (T)value;
		}
	}

	public DataEdge (Node n) : base (n) { }
}