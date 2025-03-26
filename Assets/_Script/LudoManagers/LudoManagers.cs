public class LudoManagers : Singleton<LudoManagers>
{
    #region VARS
    public LudoBoardManager BoardManager;
    public LudoGameManager GameManager;
    public LudoGameStateManager GameStateManager;
    public LudoTurnManager TurnManager;
    public LudoUIManager UIManager;
    public LudoPlayerManager PlayerManager;
    #endregion
    #region ENGINE
    void Start()
    {
        if(!BoardManager) BoardManager =         GetComponentInChildren<LudoBoardManager>();
        if(!GameManager) GameManager =           GetComponentInChildren<LudoGameManager>();
        if(!GameStateManager) GameStateManager = GetComponentInChildren<LudoGameStateManager>();
        if(!TurnManager) TurnManager =           GetComponentInChildren<LudoTurnManager>();
        if(!UIManager) UIManager =               GetComponentInChildren<LudoUIManager>();
        if(!PlayerManager) PlayerManager =       GetComponentInChildren<LudoPlayerManager>();
    }
    #endregion
}
