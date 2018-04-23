using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNear : Node {
	public DirEdge input;
	public BoolEdge output;

	public EnemyNear() {
		input = new DirEdge (this);
		output = new BoolEdge (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ output };
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.Logic;
		}
	}

	protected override IEnumerator ActivateInner () {
		bool foundEnemy = false;
		int x, y;
		Move.DirectionToAdventurerAdjacent (input.Value, out x, out y);
		if (Level.currentLevel.WithinBounds (x, y)) {
			Tile toTheRight = Level.currentLevel.GetTile (x, y);
			foundEnemy = toTheRight.MonsterHere != null;
		}
		yield return output.Activate (foundEnemy);
	}
}