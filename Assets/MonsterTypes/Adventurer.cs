using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Adventurer : Monster {
	public static Adventurer instance;

	public List<Loot> loot;

	public void OnEnable() {
		instance = this;
	}

	public override void TakeTurn () {
	}

	public override bool MoveTo (int x, int y) {
		if (!Level.currentLevel.WithinBounds (x, y)) {
			RemoveFromTile ();
			this.x = x;
			this.y = y;
			AddToTile ();
			return true;
		}
		return base.MoveTo (x, y);
	}

	public override Color MonsterColor () {
		return ColorManager.Instance.blueActive;
	}
}