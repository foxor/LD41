using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor {
	protected int selectedX = -1;
	protected int selectedY = -1;

	public override void OnInspectorGUI () {
		Level level = (Level)target;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("nodeRewards"), true);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Size: ");
		int inspectorWidth = EditorGUILayout.IntField (level.width, null);
		GUILayout.Label ("x");
		int inspectorHeight = EditorGUILayout.IntField (level.height, null);
		GUILayout.EndHorizontal ();

		if (inspectorWidth != level.width || inspectorHeight != level.height) {
			Tile[] newTiles = new Tile[inspectorWidth * inspectorHeight + inspectorHeight];
			for (int x = 0; x < level.width && x < inspectorWidth; x++) {
				for (int y = 0; y < level.height && y < inspectorHeight; y++) {
					newTiles [x + y * level.width] = level.Tiles [x + y * level.width];
				}
			}
			level.Tiles = newTiles;
			level.width = inspectorWidth;	
			level.height = inspectorHeight;
			serializedObject.Update ();
		}

		for (int y = 0; y < level.height; y++) {
			GUILayout.BeginHorizontal ();
			for (int x = 0; x < level.width; x++) {
				if (GUILayout.Button (level.Tiles[x + y * level.width].ToString ())) {
					selectedX = x;
					selectedY = y;
				}
			}
			GUILayout.EndHorizontal ();
		}

		if (selectedX >= 0 && selectedX < level.width && selectedY >= 0 && selectedY < level.height) {
			SerializedProperty tiles = serializedObject.FindProperty ("Tiles");
			SerializedProperty selectedTile = tiles.GetArrayElementAtIndex (selectedX + selectedY * level.width);
			EditorGUILayout.PropertyField (selectedTile, true);

			if (Event.current.type == EventType.KeyDown) {
				Debug.Log (Event.current.keyCode.ToString ());
				if (Event.current.keyCode == KeyCode.Alpha3) {
					level.Tiles [selectedX + selectedY * level.width].IsWall = true;
					Event.current.Use ();
					serializedObject.Update ();
				}
				if (Event.current.keyCode == KeyCode.Alpha4) {
					level.Tiles [selectedX + selectedY * level.width].IsWall = false;
					Event.current.Use ();
					serializedObject.Update ();
				}
			}
		}
		serializedObject.ApplyModifiedProperties ();
	}
}

[CustomEditor(typeof(EscapeWithLootLevel))]
public class EscapeLevelEditor : LevelEditor {
	public override void OnInspectorGUI () {
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("Treasure"), true);
		base.OnInspectorGUI ();
	}
}