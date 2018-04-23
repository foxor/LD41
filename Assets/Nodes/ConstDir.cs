using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstDir : Node {
	protected ControlFlow input;
	protected DirEdge output;

	public Direction direction;

	public ConstDir() {
		input = new ControlFlow (this);
		output = new DirEdge (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ output };
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Logic;
		}
	}

	protected override IEnumerator ActivateInner () {
		yield return output.Activate (direction);
	}

	public override void OnClicked () {
		direction = (Direction)((((int)direction) + 1) % 4);
	}

	public static string DirectionToString(Direction direction) {
		switch (direction) {
		case Direction.Down:
			return "Down";
		case Direction.Left:
			return "Left";
		case Direction.Right:
			return "Right";
		case Direction.Up:
			return "Up";
		default:
			throw new System.NotImplementedException ("Bullshit!  You added a new direction?!");
		}
	}

	public override string Name () {
		return DirectionToString (direction);
	}
}