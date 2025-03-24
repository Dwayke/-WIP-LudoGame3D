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
    }



    private void OnDisable()
    {
        if (LudoManagers.Instance != null)
        {
            LudoManagers.Instance.GameManager.OnGameStarted -= OnGameStarted;
            LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnDiceRollComplete; ;
            LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
        }
    }
    #endregion
    #region MEMBER METHODS
    private void OnDiceRollComplete(int obj)
    {
        if (LudoManagers.Instance.GameManager.isGameStarted && (LudoManagers.Instance.TurnManager.lastRolls[^1] == 6 || LudoManagers.Instance.TurnManager.CheckFreeDisks())) 
        {
            Debug.Log("Select a piece");
            gameState = EGameState.PieceSelection;
        }
    }
    public void RollAgain()
    {
        gameState = EGameState.DiceRoll;
    }
    public void SwitchTurn()
    {
        LudoManagers.Instance.GameManager.isFirstMove = false;
        if (LudoManagers.Instance.TurnManager.currentTurn != ETeam.Green) LudoManagers.Instance.TurnManager.currentTurn += 1;
        else LudoManagers.Instance.TurnManager.currentTurn = 0;
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
    #endregion
}
