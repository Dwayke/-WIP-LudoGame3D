using UnityEngine;

public class BaseDisk : MonoBehaviour
{
    #region VARS
    public EPieceState pieceState;
    public ETeam color;
    [Range(0,3)]public int index;
    public int pathIndex;
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
        LudoManagers.Instance.TurnManager.OnTurnSwitched += OnTurnSwitched;
    }
    private void OnDisable()
    {
        if(LudoManagers.Instance != null)
        LudoManagers.Instance.TurnManager.OnTurnSwitched -= OnTurnSwitched;
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
            currentTile._occupyingDiskList.Add(this);
        }      
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<LudoTile>() != null)
        {
            currentTile = collision.gameObject.GetComponent<LudoTile>();
            currentTile._occupyingDiskList.Remove(this);
        }
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnTurnSwitched(ETeam currentTurn)
    {
        if (color == currentTurn)
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
