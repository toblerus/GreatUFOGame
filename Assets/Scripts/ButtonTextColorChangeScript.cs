using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChangeScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Color highlightedColor;
    public Color deselectColor;

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = highlightedColor;

    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = deselectColor;

    }
}
