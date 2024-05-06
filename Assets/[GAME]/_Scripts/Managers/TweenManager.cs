using DG.Tweening;
using UnityEngine;

namespace Managers
{
    public class TweenManager : MonoBehaviour
    {

        #region ONENABLE & ONDISABLE   
        private void OnEnable()
        {
            DeckManager.OnDrawCard += DeckManager_OnDrawCard;
            DiceManager.OnRollDice += DiceManager_OnRollDice;
        }
        private void OnDisable()
        {
            DeckManager.OnDrawCard -= DeckManager_OnDrawCard;
            DiceManager.OnRollDice -= DiceManager_OnRollDice;
        }
        
        private void DeckManager_OnDrawCard(Transform obj,RectTransform targetPos)
        {
            CardMoveToSlot(obj, targetPos);
        }
        private void DiceManager_OnRollDice(Transform obj, RectTransform targetPos, int damage)
        {
            UseCard(obj, targetPos);
        }
        #endregion
        
        private void CardMoveToSlot(Transform obj,RectTransform targetPos)
        {
            obj.DOMove(targetPos.position, .5f).SetEase(Ease.Linear);
        }
        private void UseCard(Transform obj,RectTransform targetPos)
        {
            obj.DOMove(targetPos.position, .75f).SetEase(Ease.Linear)
                .OnComplete(() => CardScale(obj));
        }
        private void CardScale(Transform obj)
        {
            obj.DOScale(Vector3.zero, 1f).SetEase(Ease.Linear)
                .OnComplete(() => Destroy(obj.gameObject));
        }

        public void FloatingText(RectTransform obj , int damage )
        {
            obj.gameObject.SetActive(true);
            obj.DOAnchorPosY(-145, 1f).From().onComplete = () =>
            obj.gameObject.SetActive(false);

        }
    }
}
