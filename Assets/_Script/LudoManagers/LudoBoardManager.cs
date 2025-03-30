using DG.Tweening;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using FishNet.Object;

public class LudoBoardManager : NetworkBehaviour
{
    #region VARS
    [SerializeField] float _yOffset;
    public List<BaseDisk> availableDisks;
    [SerializeField] List<LudoTile> _redPath;
    [SerializeField] List<LudoTile> _bluePath;
    [SerializeField] List<LudoTile> _yellowPath;
    [SerializeField] List<LudoTile> _greenPath;
    public event Action<BaseDisk> OnMoveComplete;
    #endregion
    #region ENGINE
    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    private void Awake()
    {
        SpawnAllDisks();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        SpawnAllDisks();
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void SpawnAllDisks()
    {
        foreach (var disk in FindObjectsByType<BaseDisk>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            Spawn(disk.gameObject);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void CmdMoveDisk(BaseDisk selectedDisk, Vector3 targetLocation, int targetIndex)
    {
        List<LudoTile> currentPath = null;
        switch (selectedDisk.color)
        {
            case ETeam.Red:
                currentPath = _redPath;
                break;
            case ETeam.Blue:
                currentPath = _bluePath;
                break;
            case ETeam.Yellow:
                currentPath = _yellowPath;
                break;
            case ETeam.Green:
                currentPath = _greenPath;
                break;
            default:
                break;
        }
        if (currentPath == null)
            return;
        if (selectedDisk.currentTile.type == ETileType.Base)
        {
            selectedDisk.diskState = EDiskState.Free;
            selectedDisk.transform.DOMove(targetLocation, 0.5f);
            selectedDisk.pathIndex = targetIndex;
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            RpcMoveDisk(selectedDisk, targetLocation, targetIndex);
            OnMoveComplete.Invoke(selectedDisk);
        }
        else
        {
            float moveSpeed = 0.3f;
            selectedDisk.transform.DOMove(targetLocation, moveSpeed);
            selectedDisk.pathIndex = targetIndex;
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            RpcMoveDisk(selectedDisk, targetLocation, targetIndex);
            OnMoveComplete.Invoke(selectedDisk);
        }
    }
    public void MoveDisk(BaseDisk selectedDisk)
    {
        int lastRoll = LudoManagers.Instance.TurnManager.lastRolls[^1];
        int targetIndex = selectedDisk.pathIndex + lastRoll;
        Vector3 targetLocation;
        List<LudoTile> currentPath = null;
        switch (selectedDisk.color)
        {
            case ETeam.Red:
                currentPath = _redPath;
                break;
            case ETeam.Blue:
                currentPath = _bluePath;
                break;
            case ETeam.Yellow:
                currentPath = _yellowPath;
                break;
            case ETeam.Green:
                currentPath = _greenPath;
                break;
            default:
                break;
        }
        if (currentPath == null)
            return;
        if (selectedDisk.currentTile.type == ETileType.Base)
        {
            selectedDisk.diskState = EDiskState.Free;
            targetLocation = currentPath[targetIndex].tileTransform.position + ((Vector3.up * _yOffset) + (currentPath[targetIndex].occupyingDiskList.Count * Vector3.one));
            selectedDisk.transform.DOMove(targetLocation, 0.5f);
            selectedDisk.pathIndex = targetIndex;
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            CmdMoveDisk(selectedDisk, targetLocation, targetIndex);
            OnMoveComplete.Invoke(selectedDisk);
        }
        else
        {
            float moveSpeed = 0.3f;
            targetLocation = currentPath[targetIndex].tileTransform.position + ((Vector3.up * _yOffset) + (currentPath[targetIndex].occupyingDiskList.Count * Vector3.one));
            selectedDisk.transform.DOMove(targetLocation, moveSpeed);
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            selectedDisk.pathIndex = targetIndex;
            CmdMoveDisk(selectedDisk, targetLocation, targetIndex);
            OnMoveComplete.Invoke(selectedDisk);
        }
    }
    [ObserversRpc(ExcludeOwner = true,ExcludeServer =true)]
    private void RpcMoveDisk(BaseDisk selectedDisk, Vector3 targetLocation, int targetIndex)
    {
        List<LudoTile> currentPath = null;
        switch (selectedDisk.color)
        {
            case ETeam.Red:
                currentPath = _redPath;
                break;
            case ETeam.Blue:
                currentPath = _bluePath;
                break;
            case ETeam.Yellow:
                currentPath = _yellowPath;
                break;
            case ETeam.Green:
                currentPath = _greenPath;
                break;
            default:
                break;
        }
        if (currentPath == null)
            return;
        if (selectedDisk.currentTile.type == ETileType.Base)
        {
            selectedDisk.diskState = EDiskState.Free;
            selectedDisk.transform.DOMove(targetLocation, 0.5f);
            selectedDisk.pathIndex = targetIndex;
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            OnMoveComplete.Invoke(selectedDisk);
        }
        else
        {
            float moveSpeed = 0.3f;
            selectedDisk.transform.DOMove(targetLocation, moveSpeed);
            selectedDisk.pathIndex = targetIndex;
            foreach (BaseDisk disk in currentPath[targetIndex].occupyingDiskList)
            {
                if (disk.color != selectedDisk.color)
                {
                    disk.pathIndex = -6;
                    disk.transform.position = disk.originTile.transform.position;
                }
            }
            OnMoveComplete.Invoke(selectedDisk);
        }
    }
    #endregion
}
