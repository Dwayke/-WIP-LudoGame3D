using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LudoTile : MonoBehaviour
{
    #region VARS
    public ETileType type;
    public ETeam color;
    [Range(0,17)] public int tileIndex;
    public List<BaseDisk> occupyingDiskList;
    public Transform tileTransform;
    #endregion
    #region ENGINE
    private void Awake()
    {
        gameObject.name = $"Tile: {color} {type}, Tile Index: {tileIndex}";
        tileTransform = transform;
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
