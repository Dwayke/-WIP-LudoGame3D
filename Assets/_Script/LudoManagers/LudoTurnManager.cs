using System;
using System.Collections.Generic;
using UnityEngine;

public class LudoTurnManager : MonoBehaviour
{
    #region VARS
    public ETeam team = 0;
    public List<int> _lastRolls;
    #endregion
    #region ENGINE
    private void OnEnable()
    {
        LudoManagers.Instance.GameManager.OnDiceRollComplete += OnDiceRollComplete; 
    }
    private void OnDisable()
    {
        LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete;
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnDiceRollComplete(int lastRoll)
    {
        if (!LudoManagers.Instance.GameManager.isGameStarted&&lastRoll!=6)
        {
            if (team != ETeam.Green) team += 1;
            else team = 0;
        }
        _lastRolls.Add(lastRoll);
        Debug.Log("Last Move: "+_lastRolls[^1]);
    }
    #endregion
}
