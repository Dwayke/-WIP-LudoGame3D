using JetBrains.Annotations;
using UnityEngine;

public class BaseDisk : MonoBehaviour
{
    #region VARS
    public EPieceState pieceState;
    public ETeam color;
    public LudoTile currentTile;
    public LudoTile nextTile;
    [SerializeField] LudoTile originTile;
    [SerializeField] LudoTile homeTile;
    #endregion
    #region ENGINE
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<LudoTile>() != null)
        {
            currentTile = collision.gameObject.GetComponent<LudoTile>();
        }      
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
