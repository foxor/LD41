using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	Right,
	Up,
	Down,
	Left
}

public class DirEdge : DataEdge<Direction> {
	public DirEdge (Node node) : base(node) {
	}

	public override string Symbol {
		get {
			if (IsActive) {
				return ConstDir.DirectionToString (Value);
			}
			return "D";
		}
	}
}