using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject _board;
    [SerializeField] float _yOffset;
    public List<BaseDisk> availableDisks;
    public List<LudoTile> waypointTileList;
    public List<LudoTile> baseTileList;
    [SerializeField] List<LudoTile> _redPath;
    [SerializeField] List<LudoTile> _bluePath;
    [SerializeField] List<LudoTile> _yellowPath;
    [SerializeField] List<LudoTile> _greenPath;
    public event Action OnMoveComplete;
    #endregion
    #region ENGINE
    private void OnEnable()
    {
        LudoManagers.Instance.PlayerController.OnPieceSelected += OnPieceSelected;
    }
    private void OnDisable()
    {
        if(LudoManagers.Instance != null)
        LudoManagers.Instance.PlayerController.OnPieceSelected -= OnPieceSelected;
    }
    #endregion
    #region MEMBER METHODS
    public void AddTile(LudoTile tile)
    {
        if(tile.type == ETileType.Waypoint) waypointTileList.Add(tile); 
        if(tile.type == ETileType.Base) baseTileList.Add(tile); 
    }
    #endregion
    #region LOCAL METHODS
    private void OnPieceSelected(BaseDisk selectedPiece)
    {
        MovePiece(selectedPiece);
    }
    private void MovePiece(BaseDisk selectedDisk)
    {
        int lastRoll = LudoManagers.Instance.TurnManager.lastRolls[^1];
        Vector3 targetLocation /*= selectedDisk.currentTile.nextTile.transform.position + (Vector3.up * _yOffset)*/;
        switch (selectedDisk.color)
        {
            case ETeam.Red:
                targetLocation = _redPath[selectedDisk.pathIndex+lastRoll].tileTransform.position + (Vector3.up * _yOffset);
                selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(() => { OnMoveComplete.Invoke(); });
                break;
            case ETeam.Blue:
                targetLocation = _bluePath[selectedDisk.pathIndex + lastRoll].tileTransform.position + (Vector3.up * _yOffset);
                selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(() => { OnMoveComplete.Invoke(); });
                break;
            case ETeam.Yellow:
                targetLocation = _yellowPath[selectedDisk.pathIndex + lastRoll].tileTransform.position + (Vector3.up * _yOffset);
                selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(() => { OnMoveComplete.Invoke(); });
                break;
            case ETeam.Green:
                targetLocation = _greenPath[selectedDisk.pathIndex + lastRoll].tileTransform.position + (Vector3.up * _yOffset);
                selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(() => { OnMoveComplete.Invoke(); });
                break;
            default:
                break;
        }
        //selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(()=> { OnMoveComplete.Invoke(); });
    }
    #endregion
}
