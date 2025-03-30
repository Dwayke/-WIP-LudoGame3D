using FishNet.Object;
using UnityEngine;

public class BaseDisk : NetworkBehaviour
{
    #region VARS
    public EDiskState diskState;
    public ETeam color;
    [Range(0,3)]public int index;
    public int pathIndex;
    public LudoTile currentTile;
    public LudoTile originTile;
    #endregion
    #region ENGINE
    public override void OnStartServer()
    {
        base.OnStartServer();
        Spawn(gameObject);
        gameObject.name = $"{color} Disk, Disk Index: {index}";
        if (color == LudoManagers.Instance.TurnManager.currentTurn)
        {
            LudoManagers.Instance.BoardManager.availableDisks.Add(this);
        }
        else
        {
            LudoManagers.Instance.BoardManager.availableDisks.Remove(this);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        pathIndex = -6;
        LudoManagers.Instance.TurnManager.OnTurnSwitched += OnTurnSwitched;
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        if (LudoManagers.Instance != null)
            LudoManagers.Instance.TurnManager.OnTurnSwitched -= OnTurnSwitched;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<LudoTile>() != null)
        {
            currentTile = collision.gameObject.GetComponent<LudoTile>();
            currentTile.occupyingDiskList.Add(this);
        }      
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<LudoTile>() != null)
        {
            currentTile = collision.gameObject.GetComponent<LudoTile>();
            currentTile.occupyingDiskList.Remove(this);
        }
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void OnTurnSwitched(ETeam currentTurn)
    {
        if (color == currentTurn && diskState != EDiskState.Home)
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
