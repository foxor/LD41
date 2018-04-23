using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DirectScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public static int BlockersRefCount;
	public static bool BlockNodeDrop { get { return BlockersRefCount > 0; } }

	public ScrollRect scrollRect;

	void Update ()
	{
		scrollRect.movementType = Input.GetMouseButton (0) ? ScrollRect.MovementType.Unrestricted : ScrollRect.MovementType.Elastic;

		if (Input.GetMouseButtonUp (0)) {
			scrollRect.enabled = true;
			scrollRect.OnEndDrag (new PointerEventData(EventSystem.current));
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		scrollRect.enabled = true;
		BlockersRefCount++;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		scrollRect.enabled = false;
		BlockersRefCount--;
	}
}
