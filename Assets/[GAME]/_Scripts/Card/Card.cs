using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{  
   [SerializeField] private CardData cardData;
   [SerializeField] private TextMeshProUGUI cardName;
   [SerializeField] private TextMeshProUGUI explanation;
   [SerializeField] private TextMeshProUGUI cardValueText;
   [SerializeField] Image cardSprite;
   [SerializeField] private Image background;
   private GameObject _particlePrefab;
   
   private int _cardValue;
   private CardType _cardType;

   private void Start()
   {
      Init();
   }

   public void Init()
   {
      cardName.text = cardData.cardName;
      explanation.text = cardData.explanation;
      cardSprite.sprite = cardData.cardSprite;
      _cardValue = cardData.cardValue;
      cardValueText.text =_cardValue.ToString();
      _cardType = cardData.cardType;
      _particlePrefab = cardData.particlePrefab;
      
      background.sprite = _cardType == CardType.blue ? cardData.blue : cardData.red;
   }
   public void SetCardData(CardData newCardData)
   {
      cardData = newCardData;
   }
   public int GetCardValue()
   {
      return _cardValue;
   }
   public void InstantiateParticle()
   {
      Vector3 prefabPos;
      
      switch (_cardType)
      {
         case CardType.red:
            prefabPos = new Vector3(0, 0, 0);
            break;
         case CardType.blue:
            prefabPos = new Vector3(0, 2, 0);
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
      GameObject particle = Instantiate(_particlePrefab, prefabPos, Quaternion.identity);
   }
}


