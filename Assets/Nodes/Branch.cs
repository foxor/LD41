using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : Node
{
	private BoolEdge input;

	private ControlFlow top;
	private ControlFlow bottom;

	public Branch()
	{
		input = new BoolEdge (this);
		top = new ControlFlow (this);
		bottom = new ControlFlow (this);
		
		Inputs = new Edge[] { input };
		Outputs = new Edge[] { top, bottom };
	}

	protected override IEnumerator ActivateInner ()
	{
		if (input.Value) {
			yield return top.Activate (null);
		} else {
			yield return bottom.Activate (null);
		}
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Logic;
		}
	}
}