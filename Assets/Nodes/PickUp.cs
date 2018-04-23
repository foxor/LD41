using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Node {
	protected ControlFlow input;

	public PickUp() {
		input = new ControlFlow (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ };
	}

	protected override IEnumerator ActivateInner () {
		bool foundLoot = false;
		foreach (Loot loot in Adventurer.instance.MyTile.LootOnFloor) {
			loot.OnPickup ();
			Level.currentLevel.dirty = true;
			foundLoot = true;
		}
		Adventurer.instance.loot.AddRange (Adventurer.instance.MyTile.LootOnFloor);
		Adventurer.instance.MyTile.LootOnFloor.Clear ();
		if (foundLoot) {
			SFXPlayer.PlaySound ("Loot");
		}
		yield break;
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.CharacterAction;
		}
	}
}