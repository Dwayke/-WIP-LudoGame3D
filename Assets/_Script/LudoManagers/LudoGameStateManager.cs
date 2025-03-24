using System;
using UnityEngine;

public class LudoGameStateManager : MonoBehaviour
{
    #region VARS
    public EGameState gameState;
    #endregion
    #region ENGINE
    private void Start()
    {
        LudoManagers.Instance.GameManager.OnGameStarted += OnGameStarted;
        LudoManagers.Instance.GameManager.OnDiceRollComplete += OnDiceRollComplete; ;
        LudoManagers.Instance.PlayerController.OnPieceSelected += OnPieceSelected;
        LudoManagers.Instance.BoardManager.OnMoveComplete += OnMoveComplete;
    }
    private void OnDisable()
    {
        if (LudoManagers.Instance != null)
        {
            LudoManagers.Instance.GameManager.OnGameStarted -= OnGameStarted;
            LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete; ;
            LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
            LudoManagers.Instance.BoardManager.OnMoveComplete -= OnMoveComplete;
        }
    }
    #endregion
    #region MEMBER METHODS
    public void InitiateRollState()
    {
        gameState = EGameState.DiceRoll;
    }
    #endregion
    #region LOCAL METHODS
    private void OnGameStarted()
    {
        gameState = EGameState.PieceSelection;
    }
    private void OnDiceRollComplete(int lastRoll, ETeam lastPlayer)
    {
        if (LudoManagers.Instance.GameManager.isGameStarted && (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6 || LudoManagers.Instance.TurnManager.CheckFreeDisks()))
        {
            if(lastPlayer == LudoManagers.Instance.TurnManager.currentTurn)
            {
                Debug.Log("Select a piece");
                gameState = EGameState.PieceSelection;
            }
        }
    }
    private void OnPieceSelected(BaseDisk selectedDisk)
    {
        gameState = EGameState.PieceMotion;
    }
    private void OnMoveComplete(BaseDisk selectedDisk)
    {
        if (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6 && selectedDisk.pieceState == EPieceState.Locked)
        {
            LudoManagers.Instance.GameManager.isFirstMove = false;
            LudoManagers.Instance.TurnManager.SwitchTurn();
            gameState = EGameState.DiceRoll;
        }
        else if(LudoManagers.Instance.TurnManager.lastRolls[^1] == 6 && selectedDisk.pieceState != EPieceState.Locked)
        {
            gameState = EGameState.DiceRoll;
        }
        else if(LudoManagers.Instance.TurnManager.lastRolls[^1] != 6)
        {
            LudoManagers.Instance.TurnManager.SwitchTurn();
            gameState = EGameState.DiceRoll;
        }
    }
    #endregion
}
