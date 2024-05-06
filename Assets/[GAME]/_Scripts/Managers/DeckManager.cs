using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Managers
{
    public class DeckManager : MonoBehaviour
    {
        #region INJECTIONS

        [Inject] private GameManager gameManager;
        [Inject] private WorldHealth worldHealth;

        #endregion
        
        #region ACTIONS

        public static Action<Transform,RectTransform> OnDrawCard;
        public static Action OnGameOver;

        #endregion
        
        [HideInInspector] public List<GameObject> cardList;
        [SerializeField] private List<CardData> dataSOList;
        [SerializeField] private  GameObject cardPrefab;
        [SerializeField] private RectTransform cardContainer, drawCardPos;
        [SerializeField] private  RectTransform[] cardSlots;
        [SerializeField] private  Button drawButton;
        
        private bool _isAllSlotsFull;
        private void Start()
        {
            InstantiateCard();
            drawButton.onClick.AddListener(DrawCard);
        }
        private void Update()
        {
            _isAllSlotsFull = cardSlots.All(slot => slot.GetComponentInChildren<Card>() != null);
          
            if (cardList.Count == 0 || worldHealth.GetWorldHealth() <= 0)
            {
                OnGameOver?.Invoke();
            }
        }
        private void InstantiateCard()
        {
            foreach (CardData cardData in dataSOList)
            {
                GameObject cardObject = Instantiate(cardPrefab, cardContainer);
                Card card = cardObject.GetComponent<Card>();
                card.SetCardData(cardData);
                card.Init();
                cardList.Add(cardObject);
                cardObject.SetActive(false);
            }
        }
        private void DrawCard()
        {
            if (!gameManager.IsGameState(GameState.draw)) return;
            foreach (RectTransform position in cardSlots)
            {
                if (position.childCount == 0)
                {
                    int randomIndex = Random.Range(0, cardList.Count);
                    GameObject selectedCard = cardList[randomIndex];
                    selectedCard.SetActive(true);
                    selectedCard.transform.SetParent(position);
                    OnDrawCard?.Invoke(selectedCard.transform,position);
                    cardList.RemoveAt(randomIndex);
                    gameManager.OnGameStateChanged?.Invoke(GameState.roll);
                }
            }
        }
        public void GetCardDamageInSlot(int diceValue)
        {
            int cardIndex = diceValue-1;
            GameObject slot = cardSlots[cardIndex].gameObject;
            Card card = slot.GetComponentInChildren<Card>();
            GameObject cardObject = card.gameObject;
            int damage = card.GetCardValue();
            DiceManager.OnRollDice?.Invoke(cardObject.transform,drawCardPos,damage);
            card.InstantiateParticle();
        }
        public bool IsAllSlotFull()
        {
            return _isAllSlotsFull;
        }
        
    }
}
