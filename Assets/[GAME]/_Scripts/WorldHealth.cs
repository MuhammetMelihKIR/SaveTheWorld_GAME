using System;
using Managers;
using TMPro;
using UnityEngine;

public class WorldHealth : MonoBehaviour
{
    private int _worldHealth;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int turnDamage = -50;

    #region ONENABLE & ONDISABLE

    private void OnEnable()
    {
        DiceManager.OnRollDice += DiceManager_OnRollDice;
    }

    private void OnDisable()
    {
        DiceManager.OnRollDice -= DiceManager_OnRollDice;
    }

    private void DiceManager_OnRollDice(Transform obj, RectTransform targetPos, int damage)
    {
        UpdateWorldHealth(damage);
    }

    #endregion
    

    private void Awake()
    {
        _worldHealth = 1000;
        scoreText.text = _worldHealth.ToString();
        
    }

    public void UpdateWorldHealth(int damage)
    {
        _worldHealth += damage;
        if (_worldHealth<=0)
        {
            _worldHealth=0;
        }
        scoreText.text = _worldHealth.ToString();
    }

    public int GetWorldHealth()
    {
        return _worldHealth;
        
    }
}
