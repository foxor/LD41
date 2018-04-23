using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClearSave {
	[MenuItem("Tools/Clear PlayerPrefs")]
	private static void NewMenuOption()
	{
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}
}