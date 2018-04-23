using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node {
	protected DirEdge input;

	public Attack() {
		input = new DirEdge (this);
		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ };
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.CharacterAction;
		}
	}

	protected override IEnumerator ActivateInner () {
		int x, y;
		Move.DirectionToAdventurerAdjacent (input.Value, out x, out y);
		if (Level.currentLevel.WithinBounds (x, y)) {
			Tile toTheRight = Level.currentLevel.GetTile (x, y);
			if (toTheRight.MonsterHere != null) {
				CombatLogic.ProcessAttack (Adventurer.instance, toTheRight.MonsterHere);
			}
		}
		yield break;
	}
}