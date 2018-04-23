using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {
	public static RewardManager Instance { get; protected set; }

	public Text rewardText;

	public void Start () {
		Instance = this;
		Close ();
	}

	public void ShowReward(string reward) {
		rewardText.text = reward;
		gameObject.SetActive (true);
	}

	public void Close() {
		gameObject.SetActive (false);
	}
}