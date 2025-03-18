using System.Collections.Generic;
using UnityEngine;

public class LudoBoardManager : MonoBehaviour
{
    #region VARS
    [SerializeField] GameObject _board;
    public List<LudoTile> tileList;
    [SerializeField] GameObject _tilePrefab;
    #endregion
    #region ENGINE
    
    #endregion
    #region MEMBER METHODS
    public void AddTile(LudoTile tile)
    {
        if(tile.type == ETileType.Waypoint) tileList.Add(tile); 
    }
    #endregion
    #region LOCAL METHODS
    #endregion
}
