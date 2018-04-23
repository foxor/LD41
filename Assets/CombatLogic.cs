using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLogic {
	public static void ProcessAttack(Monster source, Monster target) {
		target.Health--;
		if (target.Health <= 0) {
			target.Destroy ();
		}

		if (target != Adventurer.instance) {
			Level.currentLevel.dirty = true;
		}

		SFXPlayer.PlaySound ("Attack");
	}
}