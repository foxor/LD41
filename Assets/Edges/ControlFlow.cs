using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFlow : Edge
{
	public ControlFlow (Node n) : base (n) { }

	public override string Symbol
	{
		get
		{
			return ">";
		}
	}
}