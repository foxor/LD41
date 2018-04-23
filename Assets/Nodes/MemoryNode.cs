using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GetNode<T> : MemoryNode<T> {
	protected ControlFlow input;
	protected DirEdge output;

	public GetNode() {
		input = new ControlFlow (this);
		output = new DirEdge (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ output };
	}

	public override string Name () {
		string value = "";
		if (Level.currentLevel != null && Level.currentLevel.memory.ContainsKey (memoryLocation)) {
			value = Level.currentLevel.memory [memoryLocation].ToString ();
		}
		return "Get: " + memoryLocation + " -> " + value;
	}

	protected override IEnumerator ActivateInner () {
		object memory = null;
		if (!Level.currentLevel.memory.TryGetValue (memoryLocation, out memory)) {
			Level.currentLevel.errorMessage = "Your adventurer doesn't remember a variable with name " + memoryLocation;
			yield break;
		}
		if (memory is T) {
			yield return output.Activate ((T)memory);
		} else {
			Level.currentLevel.errorMessage = "Your adventurer didn't know how to interpert " + memoryLocation + "=" + memory.ToString() + " as a " + typeof(T).Name;
		}
	}
}

public abstract class SetNode<T> : MemoryNode<T> {
	protected DirEdge input;

	public SetNode() {
		input = new DirEdge (this);

		Inputs = new Edge[]{ input };
		Outputs = new Edge[]{ };
	}

	protected override IEnumerator ActivateInner () {
		Level.currentLevel.memory [memoryLocation] = input.Value;
		yield break;
	}

	public override string Name () {
		return "Set: " +  memoryLocation;
	}
}

public abstract class MemoryNode<T> : Node {
	protected string memoryLocation = "a";

	public override void OnClicked () {
		memoryLocation = ((char)(((int)memoryLocation [0] + 1 - (int)'a') % 5 + (int)'a')).ToString();
	}

	protected override TimeCategory Duration {
		get {
			return TimeCategory.MemoryOperation;
		}
	}
}