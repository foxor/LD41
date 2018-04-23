using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolEdge : DataEdge<bool>
{
	public const string UNSET = "?";
	public const string TRUE = "✓";
	public const string FALSE = "X";

	public BoolEdge (Node n) : base (n) { }
	
	public override string Symbol { get { return IsActive ? (Value ? TRUE : FALSE) : UNSET; } }
}