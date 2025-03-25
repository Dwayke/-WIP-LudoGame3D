using DG.Tweening;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
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
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    public async void MoveDisk(BaseDisk selectedDisk)
    {
        int lastRoll = LudoManagers.Instance.TurnManager.lastRolls[^1];
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
            int targetIndex = selectedDisk.pathIndex + lastRoll;
            targetLocation = currentPath[targetIndex].tileTransform.position + (Vector3.up * _yOffset * currentPath[targetIndex].occupyingDiskList.Count);
            await selectedDisk.transform.DOMove(targetLocation, 0.5f).AsyncWaitForCompletion();
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
            for (int i = 1; i <= lastRoll; i++)
            {
                int targetIndex = selectedDisk.pathIndex + i;
                targetLocation = currentPath[targetIndex].tileTransform.position + (Vector3.up * _yOffset);
                await selectedDisk.transform.DOMove(targetLocation, moveSpeed).AsyncWaitForCompletion();
                await UniTask.Delay(100);
            }
            selectedDisk.pathIndex += lastRoll;
            OnMoveComplete.Invoke(selectedDisk);
        }
    }
    #endregion
}
