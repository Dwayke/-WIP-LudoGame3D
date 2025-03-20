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
    private void Start()
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
        LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete;
        LudoManagers.Instance.BoardManager.OnMoveComplete -= OnMoveComplete;
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnDiceRollComplete(int lastRoll)
    {
        if (!LudoManagers.Instance.GameManager.isGameStarted&&lastRoll!=6)
        {
            if (currentTurn != ETeam.Green) currentTurn += 1;
            else currentTurn = 0;
        }
        lastRolls.Add(lastRoll);
        Debug.Log("Last Move: "+lastRolls[^1]);
        LudoManagers.Instance.UIManager.DisplayCurrentTurn(currentTurn);
    }
    private void OnMoveComplete()
    {
        if (lastRolls[^1] == 6)
        {
            LudoManagers.Instance.GameStateManager.RollAgain();
        }
        else 
        {
            if (currentTurn != ETeam.Green) currentTurn += 1;
            else currentTurn = 0;
            LudoManagers.Instance.GameStateManager.SwitchTurn();
        }
    }
    #endregion
}
