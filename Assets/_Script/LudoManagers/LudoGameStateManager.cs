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
    }
    private void OnDisable()
    {
        LudoManagers.Instance.GameManager.OnGameStarted -= OnGameStarted;
        LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
    }
    #endregion
    #region MEMBER METHODS
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
    #endregion
}
