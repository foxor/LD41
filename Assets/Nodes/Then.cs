using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Then : Node {
	protected ControlFlow input;
	protected ControlFlow top;
	protected ControlFlow bottom;

	public Then() {
		input = new ControlFlow (this);
		top = new ControlFlow (this);
		bottom = new ControlFlow (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ top, bottom };
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Logic;
		}
	}

	protected override IEnumerator ActivateInner () {
		yield return top.Activate ();
		yield return bottom.Activate ();
	}
}