using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
	public static ColorManager Instance { get; protected set; }

	public Color blueBackground;
	public Color blueForeground;
	public Color blueActive;
	public Color enemy;
	public Color wall;
	public Color loot;

	public void Awake() {
		Instance = this;
	}
}