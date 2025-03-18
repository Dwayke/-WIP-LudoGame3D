using UnityEngine;

public class LudoManagers : Singleton<LudoManagers>
{
    #region VARS
    public LudoBoardManager BoardManager;
    public LudoGameManager GameManager;
    public LudoGameStateManager GameStateManager;
    public LudoTurnManager TurnManager;
    public LudoUIManager UIManager;
    #endregion
    #region ENGINE
    void Start()
    {
        if(!BoardManager) BoardManager = GetComponent<LudoBoardManager>();
        if(!GameManager) GameManager = GetComponent<LudoGameManager>();
        if(!GameStateManager) GameStateManager = GetComponent<LudoGameStateManager>();
        if(!TurnManager) TurnManager = GetComponent<LudoTurnManager>();
        if(!UIManager) UIManager = GetComponent<LudoUIManager>();
    }
    #endregion
}
