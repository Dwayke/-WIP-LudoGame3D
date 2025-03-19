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
    }
    private void OnDisable()
    {
        LudoManagers.Instance.GameManager.OnGameStarted -= OnGameStarted;
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnGameStarted()
    {
        gameState = EGameState.DiceRoll;
    }
    #endregion
}
