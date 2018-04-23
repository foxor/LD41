using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
	public const string SAVE_KEY = "PlayerState";
	public const int GENERATED_LEVELS = 0;

	public static Game Instance { get; protected set; }

	public PlayerState initialState;
	public GameObject storyScreen;
	public GameObject winScreen;
	public GameObject rougelikeParent;
	public GameObject nodeToolbox;
	public Text storyText;

	public string[] Story = new string[0];
	public Level[] FixedLevels = new Level[0];

	void Start () {
		Instance = this;
		PlayerState.instance = ScriptableObject.CreateInstance<PlayerState> ();
		string data = JsonUtility.ToJson (initialState.data);
		if (PlayerPrefs.HasKey (SAVE_KEY)) {
			data = PlayerPrefs.GetString (SAVE_KEY);
		}
		PlayerState.instance.data = JsonUtility.FromJson<PlayerStateData> (data);

		TryStory ();
	}

	public void Save() {
		PlayerPrefs.SetString (SAVE_KEY, JsonUtility.ToJson (PlayerState.instance.data));
		PlayerPrefs.Save ();
	}

	public void AdvanceStory() {
		PlayerState.instance.data.CurrentStory++;
		Save ();
		TryStory ();
	}

	protected void TryStory() {
		if (PlayerState.instance.data.CurrentStory < Story.Length) {
			storyText.text = Story [PlayerState.instance.data.CurrentStory];
		} else {
			storyScreen.SetActive (false);
			NodeList.Instance.Reinitalize ();
		}
	}

	protected void BackToEditMode() {
		nodeToolbox.SetActive (true);
		rougelikeParent.SetActive (false);
		NodeList.Instance.Reinitalize ();
		Level.currentLevel = null;
	}

	public void AdvanceLevel() {
		PlayerState.instance.data.CurrentLevel++;
		PlayerState.instance.data.UnlockedNodeTypes.AddRange (Level.currentLevel.nodeRewards);
		if (Level.currentLevel.nodeRewards.Length > 0) {
			RewardManager.Instance.ShowReward (string.Join(", ", Level.currentLevel.nodeRewards));
		} else {
			// TODO: Other rewards?
		}
		if (HasLevels) {
			Save ();
			BackToEditMode ();
		} else {
			WinGame ();
		}
	}

	protected void SetupLevel(Level level) {
		if (Always.StartPoint == null) {
			return;
		}

		for (int i = rougelikeParent.transform.childCount - 1; i >= 0; i--) {
			Destroy (rougelikeParent.transform.GetChild (i).gameObject);
		}

		Level.currentLevel = (Level)ScriptableObject.Instantiate(level);
		GameObject columnResource = Resources.Load<GameObject> ("Column");
		GameObject tileResource = Resources.Load<GameObject> ("Tile");
		for (int x = 0; x < level.width; x++) {
			Transform column = GameObject.Instantiate<GameObject> (columnResource, rougelikeParent.transform).transform;
			for (int y = 0; y < level.height; y++) {
				TileView tileView = GameObject.Instantiate<GameObject> (tileResource, column).GetComponent<TileView> ();
				tileView.x = x;
				tileView.y = y;

				Tile tile = Level.currentLevel.GetTile (x, y);
				tile.x = x;
				tile.y = y;
				// These are all scriptable objects, so we want to instantiate new copies of them and make sure they have the right position
				tile.SyncContents ();
			}
		}
		nodeToolbox.SetActive (false);
		DirectScroll.BlockersRefCount = 0; // Gross
		rougelikeParent.SetActive (true);
		StartCoroutine (ProcessLevel ());
	}

	public void TryFixedLevel() {
		if (PlayerState.instance.data.CurrentLevel < FixedLevels.Length) {
			SetupLevel (FixedLevels [PlayerState.instance.data.CurrentLevel]);
		} else {
			TryGeneratedLevel ();
		}
	}

	protected bool HasLevels {
		get {
			int generatedLevelIndex = PlayerState.instance.data.CurrentLevel - FixedLevels.Length;
			return generatedLevelIndex < GENERATED_LEVELS;
		}
	}

	public void TryGeneratedLevel() {
		if (HasLevels) {
			// TODO: Generated levels
			throw new NotImplementedException();
		} else {
			WinGame ();
		}
	}

	public IEnumerator ProcessLevel() {
		foreach (Node setupNode in StartNode.StartPoints) {
			yield return setupNode.Activate ();
			if (Level.currentLevel.dirty) {
				Level.currentLevel.errorMessage = "Your adventurer wasn't ready!";
			}
		}
		while (!Level.currentLevel.isOver) {
			Level.currentLevel.dirty = false;
			yield return Always.StartPoint.Activate ();
			if (!Level.currentLevel.dirty) {
				break;
			}
		}
		if (Level.currentLevel.win) {
			AdvanceLevel ();
		} else {
			string reason = "Your adventurer failed to complete the level";
			if (Adventurer.instance.Health == 0) {
				reason = "Your adventurer died!";
			} else if (!Level.currentLevel.dirty) {
				reason = "Your adventurer didn't know what to do next";
			}
			if (!string.IsNullOrEmpty (Level.currentLevel.errorMessage)) {
				reason = Level.currentLevel.errorMessage;
			}
			FailureController.Instance.ShowFailurePopup (reason);
			SFXPlayer.PlaySound ("Failure");
			BackToEditMode ();
		}
	}

	public void WinGame() {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save ();
		winScreen.SetActive (true);
	}

	void OnEnable() {
		Application.logMessageReceived += HandleLog;
	}
	void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}
	protected void HandleLog(string logString, string stackTrace, LogType type) {
		if (Level.currentLevel != null) {
			if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert) {
				Level.currentLevel.errorMessage = "Your adventurer appears, but is only able to say the words: \"" + logString + "\" for the rest of his life.";
			}
		}
	}
}