using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LudoTile : MonoBehaviour
{
    #region VARS
    public ETileType type;
    public ETeam color;
    [Range(0,17)] public int tileIndex;
    [SerializeField] List<BaseDisk> _occupyingDiskList;
    public Transform tileTransform;
    public LudoTile nextTile;
    #endregion
    #region ENGINE
    private void Awake()
    {
        gameObject.name = $"Tile: {color} {type}, Tile Index: {tileIndex}";
        FindNextTile();
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    private void FindNextTile()
    {
        if (type == ETileType.Base)
        {
            nextTile = GameObject.Find($"Tile: {color} {type + 1}, Tile Index: {8}").GetComponent<LudoTile>();
        }
        else if (type == ETileType.Waypoint)
        {
            if(tileIndex + 1 <= 12)
            {
                nextTile = GameObject.Find($"Tile: {color} {type}, Tile Index: {tileIndex + 1}").GetComponent<LudoTile>();
            }
            else if(tileIndex+1<=17)
            {
                nextTile = GameObject.Find($"Tile: {color} {type}, Tile Index: {tileIndex + 1}").GetComponent<LudoTile>();
            }
        }
        else if(type == ETileType.Home)
        {
            nextTile = null;
        }
    }
    #endregion
}
