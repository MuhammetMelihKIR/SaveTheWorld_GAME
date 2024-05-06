using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
   

    private void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
       rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        
        rectTransform.anchoredPosition = startPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        if (eventData.pointerDrag != null && eventData.pointerDrag != gameObject)
        {
            if (!eventData.pointerCurrentRaycast.isValid)
            {
                rectTransform.anchoredPosition = startPosition;
            }
        }
    }
    
}
