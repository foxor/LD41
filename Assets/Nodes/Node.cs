using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Node
{
	public bool IsActive {
		get {
			return ActiveRefCount > 0;
		}
	}

	public Edge[] Inputs { get; protected set; }
	public Edge[] Outputs { get; protected set; }

	protected int ActiveRefCount;

	protected abstract IEnumerator ActivateInner ();

	protected abstract TimeCategory Duration { get; }

	public IEnumerator Activate ()
	{
		bool isLast = Inputs.All (x => x.IsActive);
		while (!Inputs.All (x => x.IsActive)) {
			yield return null;
		}
		if (isLast) {
			ActiveRefCount++;
			yield return TimingController.Instance.WaitAppropriately (Duration);
			if (Duration != TimeCategory.Math) {
				SFXPlayer.PlaySound ("ActivateNode");
			}
			yield return ActivateInner ();
			ActiveRefCount--;
		}
	}

	public virtual string Name()
	{
		return GetType ().Name;
	}

	public virtual void OnClicked() { }
}