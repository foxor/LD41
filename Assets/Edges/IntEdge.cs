using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntEdge : DataEdge<int>
{
	public override string Symbol {
		get {
			return Value.ToString ();
		}
	}

	public IntEdge (Node n) : base (n) { }
}