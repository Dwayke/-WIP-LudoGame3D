using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TileDebug : MonoBehaviour
{
    #region VARS
    [SerializeField] TMP_Text _tileText;
    #endregion
    #region ENGINE
    private void Start()
    {
        _tileText = GetComponentInChildren<TMP_Text>();
        WaypointTile tile = GetComponent<WaypointTile>();
        _tileText.text = $"{tile.color} \n {tile.tileIndex}";
    }
    #endregion
}
