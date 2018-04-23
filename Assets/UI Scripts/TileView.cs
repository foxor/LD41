using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileView : MonoBehaviour {
	public int x;
	public int y;

	public Text Symbol;

	public void Update() {
		Symbol.text = Level.currentLevel.GetTile (x, y).ToString ();
		Symbol.color = Level.currentLevel.GetTile (x, y).TextColor ();
	}
}