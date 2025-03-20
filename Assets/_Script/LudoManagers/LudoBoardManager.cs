using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject _board;
    [SerializeField] float _yOffset;
    public List<LudoTile> waypointTileList;
    public List<LudoTile> baseTileList;
    public event Action OnMoveComplete;
    #endregion
    #region ENGINE
    private void OnEnable()
    {
        LudoManagers.Instance.PlayerController.OnPieceSelected += OnPieceSelected;
    }
    private void OnDisable()
    {
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
        Vector3 targetLocation = selectedDisk.currentTile.nextTile.transform.position + (Vector3.up*_yOffset);
        //selectedDisk.transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * 10);
        selectedDisk.transform.DOMove(targetLocation, Time.deltaTime * 10).OnComplete(()=> { OnMoveComplete.Invoke(); });
    }
    #endregion
}
