using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigmoid {
	public static float Sample(float min, float max, float maxSlope, float value) {
		float x = value * maxSlope;
		float delta = max - min;
		float innerSample = 1f / (1f + Mathf.Exp (-x));
		return innerSample * delta + min;
	}
}