using UnityEngine;

public class BaseDisk : MonoBehaviour
{
    #region VARS
    public EPieceState pieceState;
    public ETeam color;
    [Range(0,3)]public int index;
    public int pathIndex = -6;
    public LudoTile currentTile;
    public LudoTile nextTile;
    [SerializeField] LudoTile originTile;
    [SerializeField] LudoTile homeTile;
    #endregion
    #region ENGINE
    private void Start()
    {
        pathIndex = -6;
        if (color == LudoManagers.Instance.TurnManager.currentTurn)
        {
            LudoManagers.Instance.BoardManager.availableDisks.Add(this);
        }
        else
        {
            LudoManagers.Instance.BoardManager.availableDisks.Remove(this);
        }
        LudoManagers.Instance.GameManager.OnDiceRollComplete += OnRollComplete;
    }
    private void OnDisable()
    {
        if(LudoManagers.Instance != null)
        LudoManagers.Instance.GameManager.OnDiceRollComplete -= OnRollComplete;
    }
    private void Awake()
    {
        gameObject.name = $"{color} Disk, Disk Index: {index}";
    }
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
    private void OnRollComplete(int lastRoll)
    {
        if (color == LudoManagers.Instance.TurnManager.currentTurn)
        {
            LudoManagers.Instance.BoardManager.availableDisks.Add(this);
        }
        else 
        {
            LudoManagers.Instance.BoardManager.availableDisks.Remove(this);
        }
    }
    #endregion
}
