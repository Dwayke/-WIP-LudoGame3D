public class WaypointTile : LudoTile
{
    #region VARS
    #endregion
    #region ENGINE
    private void Start()
    {
        LudoManagers.Instance.BoardManager.AddTile(this);
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
