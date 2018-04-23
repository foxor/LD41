using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOverLoot : Node {
	protected ControlFlow input;
	protected BoolEdge output;

	public IsOverLoot() {
		input = new ControlFlow (this);
		output = new BoolEdge (this);

		Inputs = new Edge[] { input };
		Outputs = new Edge[] { output };
	}

	protected override IEnumerator ActivateInner () {
		yield return output.Activate (Adventurer.instance.MyTile.LootOnFloor.Count > 0);
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Logic;
		}
	}
}