using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class MenuManager : MonoBehaviour
{
    #region MENU PANEL

    [Header("MENU PANEL")] 
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private RectTransform howToPlayPanel;
    [SerializeField] private RectTransform buttons;
    [SerializeField] private RectTransform tittle;
    [SerializeField] private RectTransform optionsPanel;
    [SerializeField] private Transform world;
    [SerializeField] private float revealDuration;
    [SerializeField] private Button startButton,quitButton;

    #endregion

    #region IN GAME PANEL

    [Header("IN GAME PANELS")]
    [SerializeField] private RectTransform inGamePanel;
    [SerializeField] private RectTransform topPanel;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform rightPanel;
    [SerializeField] private RectTransform bottomPanel;

    #endregion

    #region GAME OVER PANEL

    [Header("GAME OVER PANEL")]
    [SerializeField] private Button restartButton;

    #endregion
   
    private void Start()
    {
        Init();
        startButton.onClick.AddListener(StartButton);
        quitButton.onClick.AddListener(QuitButton);
        restartButton.onClick.AddListener(RestartButton);
    }

    private void Init()
    {
        howToPlayPanel.DOAnchorPosX(350, revealDuration).From();
        buttons.DOAnchorPosY(-700, revealDuration).From();
        tittle.DOAnchorPosY(700, revealDuration).From();
        world.DOMove(new Vector3(0, 5, 0), revealDuration).From();
        optionsPanel.DOAnchorPosX(-2000, revealDuration).From();
    }

    private void StartButton()
    {
        howToPlayPanel.DOAnchorPosX(350, revealDuration);
        buttons.DOAnchorPosY(-700, revealDuration);
        tittle.DOAnchorPosY(700, revealDuration);
        optionsPanel.DOAnchorPosX(-2000, revealDuration)
            .onComplete = () => menuPanel.gameObject.SetActive(false);
        inGamePanel.gameObject.SetActive(true);
        topPanel.DOScale(Vector3.zero, revealDuration).From();
        leftPanel.DOScale(Vector3.zero, revealDuration).From();
        rightPanel.DOScale(Vector3.zero, revealDuration).From();
        bottomPanel.DOScale(Vector3.zero, revealDuration).From();
        
    }

    private void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    private void QuitButton()
    {
        Application.Quit();
    }
}
