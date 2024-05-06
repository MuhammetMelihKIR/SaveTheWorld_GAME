using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Drop : MonoBehaviour,IDropHandler
{
   public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Card selectedCard = eventData.pointerDrag.GetComponent<Card>();
            
            Transform selectedCardParent = selectedCard.transform.parent;

            Transform targetSlotParent = transform.parent;

            selectedCard.transform.SetParent(targetSlotParent);
            selectedCard.transform.position = targetSlotParent.position;

            transform.SetParent(selectedCardParent);
            transform.position = selectedCardParent.position;
            
        }
        
    }
}
