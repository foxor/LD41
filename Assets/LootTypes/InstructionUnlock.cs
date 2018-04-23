using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class InstructionUnlock : Loot {
	public string[] InstructionTypeNames;

	public override void OnPickup () {
		Level.currentLevel.nodeRewards = Level.currentLevel.nodeRewards.Concat (InstructionTypeNames).ToArray ();
	}
}