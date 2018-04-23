using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : Node {
	public static List<StartNode> StartPoints = new List<StartNode>();

	private ControlFlow outEdge;

	public StartNode() {
		outEdge = new ControlFlow (this);

		Inputs = new Edge[] { };
		Outputs = new Edge[] { outEdge };
	}

	public void RegisterTick() {
		StartPoints.Add (this);
	}

	~StartNode() {
		StartPoints.Remove (this);
	}

	protected override IEnumerator ActivateInner ()
	{
		yield return outEdge.Activate (null);
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Math;
		}
	}
}