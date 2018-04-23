using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstBool : Node
{
	public bool Value = true;
	
	private ControlFlow input;
	private BoolEdge output;

	public ConstBool()
	{
		input = new ControlFlow (this);
		output = new BoolEdge (this);
		
		Inputs = new Edge[] { input };
		Outputs = new Edge[] { output };
	}

	public override string Name ()
	{
		return Value ? BoolEdge.TRUE : BoolEdge.FALSE;
	}

	public override void OnClicked ()
	{
		Value = !Value;
	}

	protected override IEnumerator ActivateInner ()
	{
		yield return output.Activate (Value);
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Math;
		}
	}
}