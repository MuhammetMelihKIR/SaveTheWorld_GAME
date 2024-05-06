using Managers;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    #region AUDIO SOURCES

    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource effects;

    #endregion

    #region SLIDERS

    [Header("SLIDERS")]
    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider effectsSlider;

    #endregion

    #region AUDIO CLIPS

    [Header("AUDIO CLIPS")]
    [SerializeField] private AudioClip drawCard;
    [SerializeField] private AudioClip rollDice;

    #endregion

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
    private void DeckManager_OnDrawCard(Transform obj, RectTransform targetPos)
    {
        PlaySound(drawCard);
    }
    private void DiceManager_OnRollDice(Transform obj, RectTransform targetPos, int damage)
    {
        PlaySound(rollDice);
    }

    #endregion    
 

    private void Start()
    {
       backgroundMusicSlider.value = backgroundMusic.volume;
       backgroundMusicSlider.onValueChanged.AddListener(UpdateMusicSliderValues);
       effectsSlider.value = effects.volume;
       effectsSlider.onValueChanged.AddListener(UpdateEffectsSliderValues);
    }

    private void UpdateEffectsSliderValues(float volume)
    {
        effects.volume = volume;
    }
    private void UpdateMusicSliderValues(float volume)
    {
       backgroundMusic.volume = volume;
    }
    private void PlaySound(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }
    
}
