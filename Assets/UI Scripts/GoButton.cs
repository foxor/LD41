using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public void OnPointerEnter(PointerEventData eventData)
	{
		DirectScroll.BlockersRefCount++;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		DirectScroll.BlockersRefCount--;
	}
}