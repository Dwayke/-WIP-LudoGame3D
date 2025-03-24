public class BaseTile : LudoTile
{
    #region VARS
    #endregion
    #region ENGINE
    private void Start()
    {
        if (LudoManagers.Instance != null)
        {
            LudoManagers.Instance.BoardManager.AddTile(this);
        }
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
