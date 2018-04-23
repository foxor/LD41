using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class EscapeWithLootLevel : Level {
	public Loot Treasure;

	protected bool hasTreasure {
		get {
			return Adventurer.instance.loot.Any (x => x.GetType () == Treasure.GetType ());
		}
	}

	public override bool win {
		get {
			return base.win && hasTreasure;
		}
	}

	public override bool lose {
		get {
			return base.lose || (base.win && !hasTreasure);
		}
	}
}