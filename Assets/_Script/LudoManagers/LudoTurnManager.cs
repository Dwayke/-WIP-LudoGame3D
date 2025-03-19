using System.Collections.Generic;
using UnityEngine;

public class LudoTurnManager : MonoBehaviour
{
    #region VARS
    public ETeam currentTurn = 0;
    public List<int> _lastRolls;
    #endregion
    #region ENGINE
    private void Start()
    {
        LudoManagers.Instance.UIManager.DisplayCurrentTurn(currentTurn);
    }
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
            if (currentTurn != ETeam.Green) currentTurn += 1;
            else currentTurn = 0;
        }
        _lastRolls.Add(lastRoll);
        Debug.Log("Last Move: "+_lastRolls[^1]);
        LudoManagers.Instance.UIManager.DisplayCurrentTurn(currentTurn);
    }
    #endregion
}
