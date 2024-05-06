using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region INJECTIONS
        [Inject] GameManager gameManager;
        [Inject] WorldHealth worldHealth;
        [Inject] DeckManager deckManager;
        [Inject] TweenManager tweenManager;
        #endregion
        
        [SerializeField] private GameObject menuPanel,inGamePanel,gameOverPanel;
        [SerializeField] private Button turnButton;
        [SerializeField] private TextMeshProUGUI turnRemainingText;
        [SerializeField] private TextMeshProUGUI floatingText,scoreText;

        #region OnEnable & OnDisable
        
        private void OnEnable()
        {
            DiceManager.OnRollDice += DiceManager_OnRollDice;
            DeckManager.OnGameOver += DeckManager_OnGameOver;
        }
        private void OnDisable()
        {
            DiceManager.OnRollDice -= DiceManager_OnRollDice;
            DeckManager.OnGameOver -= DeckManager_OnGameOver;
        }
        
        private void DiceManager_OnRollDice(Transform arg1, RectTransform arg2, int damage)
        {
            floatingText.text = damage.ToString();
            tweenManager.FloatingText(floatingText.rectTransform, damage);
        }
        private void DeckManager_OnGameOver()
        {
            GameOver();
        }

        #endregion
       
        private void Start()
        {
            menuPanel.SetActive(true);
            turnRemainingText.text = "Turn: 13 ";
            turnButton.onClick.AddListener(NextTurn);
        }

        public void NextTurn()
        {
            if (!gameManager.IsGameState(GameState.play)) return;
            
            turnRemainingText.text = "Turn: " + (deckManager.cardList.Count-1);
            floatingText.text = worldHealth.turnDamage.ToString();
            
            tweenManager.FloatingText(floatingText.rectTransform, worldHealth.turnDamage);
            worldHealth.UpdateWorldHealth(worldHealth.turnDamage);
                
            gameManager.OnGameStateChanged?.Invoke(GameState.draw);
        }
        private void GameOver()
        {
            scoreText.text = " The world has " + worldHealth.GetWorldHealth().ToString() + " years left to live";
            menuPanel.SetActive(false);
            inGamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }
}
