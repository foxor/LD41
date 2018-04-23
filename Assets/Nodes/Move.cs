using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Node {
	private DirEdge input;

	public Move() {
		input = new DirEdge (this);

		Inputs = new Edge[] { input };
		Outputs = new Edge[] { };
	}

	public static void DirectionToAdventurerAdjacent(Direction direction, out int x, out int y) {
		x = Adventurer.instance.x;
		y = Adventurer.instance.y;
		int dx, dy;
		DirectionToDeltas (direction, out dx, out dy);
		x += dx;
		y += dy;
	}

	public static void DirectionToDeltas(Direction direction, out int dx, out int dy) {
		dx = 0;
		dy = 0;
		switch (direction) {
		case Direction.Down:
			dy = 1;
			break;
		case Direction.Up:
			dy = -1;
			break;
		case Direction.Right:
			dx = 1;
			break;
		case Direction.Left:
			dx = -1;
			break;
		}
	}

	public static void ExecuteMove(Direction direction) {
		int dx = 0, dy = 0;
		DirectionToDeltas (direction, out dx, out dy);
		Level.currentLevel.dirty |= Adventurer.instance.MoveBy (dx, dy);
	}

	protected override IEnumerator ActivateInner () {
		ExecuteMove ((Direction)input.value);
		yield break;
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.CharacterAction;
		}
	}
}