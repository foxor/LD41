using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeCategory {
	Math,
	StartNode,
	Logic,
	MemoryOperation,
	CharacterAction
}

public class TimingController : MonoBehaviour {
	public static TimingController Instance { get; protected set; }

	public void Awake() {
		Instance = this;
	}

	public IEnumerator WaitAppropriately(TimeCategory timeCategory) {
		switch (timeCategory) {
		case TimeCategory.Math:
			yield break;
		case TimeCategory.Logic:
			yield return new WaitForSeconds (0.2f);
			break;
		case TimeCategory.MemoryOperation:
			yield return new WaitForSeconds (0.3f);
			break;
		case TimeCategory.StartNode:
			yield return new WaitForSeconds (0.4f);
			break;
		case TimeCategory.CharacterAction:
			yield return new WaitForSeconds (0.8f);
			break;
		}
	}
}