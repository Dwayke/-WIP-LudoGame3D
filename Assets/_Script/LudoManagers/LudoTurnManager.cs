using System;
using System.Collections.Generic;
using UnityEngine;

public class LudoTurnManager : MonoBehaviour
{
    #region VARS
    public ETeam currentTurn = 0;
    public List<int> lastRolls;
    #endregion
    #region ENGINE
    private void Update()
    {
        LudoManagers.Instance.UIManager.DisplayCurrentTurn(currentTurn);
    }
    private void OnEnable()
    {
        LudoManagers.Instance.GameManager.OnDiceRollComplete += OnDiceRollComplete;
        LudoManagers.Instance.BoardManager.OnMoveComplete += OnMoveComplete;
    }
    private void OnDisable()
    {
        if (LudoManagers.Instance != null)
        {
            LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete;
            LudoManagers.Instance.BoardManager.OnMoveComplete -= OnMoveComplete;
        }
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnDiceRollComplete(int lastRoll)
    {
        lastRolls.Add(lastRoll);
        if (!LudoManagers.Instance.GameManager.isGameStarted&&lastRoll!=6)
        {
            if (currentTurn != ETeam.Green) currentTurn += 1;
            else currentTurn = 0;
        }
        if (!CheckFreeDisks() && !LudoManagers.Instance.GameManager.isFirstMove && lastRoll != 6)
        {
            if (currentTurn != ETeam.Green) currentTurn += 1;
            else currentTurn = 0;
            LudoManagers.Instance.GameStateManager.RollAgain();
        }
        Debug.Log("Last Move: "+lastRolls[^1]);
    }
    private void OnMoveComplete()
    {
        if (lastRolls[^1] == 6 && !LudoManagers.Instance.GameManager.isFirstMove)
        {
            LudoManagers.Instance.GameStateManager.RollAgain();
        }
        else 
        {
            LudoManagers.Instance.GameStateManager.SwitchTurn();
        }
    }
    public bool CheckFreeDisks()
    {
        foreach (var disk in LudoManagers.Instance.BoardManager.availableDisks)
        {
            if (disk.pieceState == EPieceState.Free) return true;
        }
        return false;
    }
    #endregion
}
