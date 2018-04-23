using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateData {
	public List<string> UnlockedNodeTypes = new List<string>();
	public int CurrentLevel;
	public int CurrentStory;
}

[CreateAssetMenu]
public class PlayerState : ScriptableObject {
	public static PlayerState instance;

	public PlayerStateData data;
}