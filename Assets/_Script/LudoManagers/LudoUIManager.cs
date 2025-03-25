using TMPro;
using UnityEngine;

public class LudoUIManager : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject _rollButton;
    [SerializeField] TMP_Text _stateText;
    [SerializeField] TMP_Text _turnText;
    [SerializeField] GameObject _victorUI;
    [SerializeField] TMP_Text _victorText;
    #endregion
    #region ENGINE
    private void Update()
    {
        _stateText.text = $"{LudoManagers.Instance.GameStateManager.gameState}";
        if(LudoManagers.Instance.GameStateManager.gameState==EGameState.DiceRoll || LudoManagers.Instance.GameStateManager.gameState == EGameState.FirstPlayerSelection)
        {
            _rollButton.SetActive(true);
        }
        else
        {
            _rollButton.SetActive(false);
        }
    }
    #endregion
    #region MEMBER METHODS
    public void DisplayCurrentTurn(ETeam currentTurn)
    {
        _turnText.text = $"{currentTurn} Turn";
        switch (currentTurn)
        {
            case ETeam.Red:
                _turnText.color = Color.red;
                break;
            case ETeam.Blue:
                _turnText.color = Color.blue;
                break;
            case ETeam.Yellow:
                _turnText.color = Color.yellow;
                break;
            case ETeam.Green:
                _turnText.color = Color.green;
                break;
            default:
                break;
        }
    }
    public void DisplayWinner(ETeam winner)
    {
        _victorUI.SetActive(true);
        _victorText.text = $"{winner} Player Has Won!";
    }
    #endregion
    #region LOCAL METHODS
    #endregion
}
