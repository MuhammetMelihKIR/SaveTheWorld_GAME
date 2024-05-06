using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Managers
{
    public enum DiceType
    {
        normal,even,odd
    }
    public class DiceManager : MonoBehaviour
    {
        public static Action<Transform,RectTransform,int> OnRollDice;

        #region INJECTIONS  
        
        [Inject] private GameManager gameManager;
        [Inject] private DeckManager deckManager;
        
        #endregion

        #region BUTTONS

        [Header("BUTTONS")]
        [SerializeField] private Button evenButton;
        [SerializeField] private Button oddButton;
        [SerializeField] private Button rollButton;
        [Space(5)]

        #endregion

        #region BUTTON VARIABLES
        
        [Header("BUTTONS VARIABLES")]
        [SerializeField] private TextMeshProUGUI evenButtonText;
        [SerializeField] private TextMeshProUGUI oddButtonText;
        private int _evenCount = 1;
        private int _oddCount = 1;
        [Space(5)]

        #endregion
        
        [SerializeField] private Image diceImage;
        private DiceType _diceType;

        private Dictionary<int, Sprite> _diceSprites = new Dictionary<int, Sprite>();
        private void Awake()
        {
            for (int i = 1; i <= 6; i++)
            {
                _diceSprites.Add(i, Resources.Load<Sprite>("DiceSprites/dice" + i));
            }
        }
        void Start()
        {
            evenButton.onClick.AddListener(EvenDiceButton);
            oddButton.onClick.AddListener(OddDiceButton);
            rollButton.onClick.AddListener(OnRollButtonClick);
        }

        private void EvenDiceButton()
        {
            _diceType = DiceType.even;
            _evenCount--;
            evenButtonText.text = _evenCount.ToString();
            evenButton.interactable = false;
        }

        private void OddDiceButton()
        {
            _diceType = DiceType.odd;
            _oddCount--;
            oddButtonText.text = _oddCount.ToString();
            oddButton.interactable = false;
        }
        
        private void OnRollButtonClick()
        {
            if (gameManager.IsGameState(GameState.roll) &&  deckManager.IsAllSlotFull())
            {
                switch (_diceType)
                {
                    case DiceType.normal:
                        RollDice(new[] {1, 2, 3, 4, 5, 6});
                        break;
                    case DiceType.even:
                        RollDice(new[] {2, 4, 6});
                        break;
                    case DiceType.odd:
                        RollDice(new[] {1, 3, 5});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_diceType), "diceType not found");
                }
            }
        }
        private void RollDice(int[] values)
        {
            int index = Random.Range(0, values.Length);
            int diceValue = values[index];
            SetDiceSprite(diceValue);
            deckManager.GetCardDamageInSlot(diceValue);
            _diceType = DiceType.normal;
            gameManager.OnGameStateChanged?.Invoke(GameState.play);
        }
        private void SetDiceSprite(int value)
        {
            if (_diceSprites.TryGetValue(value, out var sprite))
            {
                diceImage.sprite = sprite;
            }
        }
    }
}   
      

