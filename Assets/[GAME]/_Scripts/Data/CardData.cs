using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardData", order = 1)]
public class CardData : ScriptableObject
{
   public string cardName;
   public Sprite cardSprite;
   public GameObject particlePrefab;
   public string explanation;
   public int cardValue;
   public CardType cardType;
   public Sprite blue, red;
   
}
public enum CardType { blue,red }
