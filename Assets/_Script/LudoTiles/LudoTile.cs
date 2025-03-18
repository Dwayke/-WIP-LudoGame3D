using System.Collections.Generic;
using UnityEngine;

public class LudoTile : MonoBehaviour
{
    #region VARS
    public ETileType type;
    public ETileColor color;
    [Range(0,17)] public int tileIndex;
    [SerializeField] List<BaseDisk> _occupyingDiskList;
    private Transform _tileTransform;
    private LudoTile _nextTile;
    #endregion
    #region ENGINE
    private void Awake()
    {
        gameObject.name = $"Tile: {color} {type}, Tile Index: {tileIndex}";
    }
    private void Start()
    {
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
