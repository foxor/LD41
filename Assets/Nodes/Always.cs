using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Always : Node
{
	public static Always StartPoint;
	
	private ControlFlow outEdge;

	public Always() {
		outEdge = new ControlFlow (this);
		
		Inputs = new Edge[] { };
		Outputs = new Edge[] { outEdge };
	}

	public void RegisterTick() {
		StartPoint = this;
	}

	~Always() {
		if (StartPoint == this) {
			StartPoint = null;
		}
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