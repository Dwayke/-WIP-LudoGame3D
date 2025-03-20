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
        LudoManagers.Instance.PlayerController.OnPieceSelected += OnPieceSelected;
        LudoManagers.Instance.BoardManager.OnMoveComplete += OnMoveComplete;
    }
    private void OnDisable()
    {
        LudoManagers.Instance.GameManager.OnGameStarted -= OnGameStarted;
        LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
        LudoManagers.Instance.BoardManager.OnMoveComplete -= OnMoveComplete;
    }
    #endregion
    #region MEMBER METHODS
    public void RollAgain()
    {
        gameState = EGameState.DiceRoll;
    }
    public void SwitchTurn()
    {
        gameState = EGameState.DiceRoll;
    }
    #endregion
    #region LOCAL METHODS
    private void OnPieceSelected(BaseDisk obj)
    {
        gameState = EGameState.PieceMotion;
    }
    private void OnGameStarted()
    {
        gameState = EGameState.PieceSelection;
    }
    private void OnMoveComplete()
    {
        gameState = EGameState.NextPlayerDetermination;
    }
    #endregion
}
