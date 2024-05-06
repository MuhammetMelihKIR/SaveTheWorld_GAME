using System;
using UnityEngine;
public enum GameState
{
    draw,roll,play
}
public class GameManager : MonoBehaviour
{
    public  Action<GameState> OnGameStateChanged;
    
    private GameState _gameState;

    #region ONENABLE & ONDISABLE

    private void OnEnable() {
        OnGameStateChanged += SetGameState;
    }
    private void OnDisable() {
        OnGameStateChanged -= SetGameState;
    }

    #endregion
    
    private void SetGameState(GameState gameState) {
        _gameState = gameState;
    }
    public bool IsGameState(GameState gameState) {
        return _gameState == gameState;
    }
    
}
