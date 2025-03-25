using System;

public class HomeTile : LudoTile
{
    #region VARS
    #endregion
    #region ENGINE
    private void Update()
    {
        if (occupyingDiskList.Count==4)
        {
            LudoManagers.Instance.GameManager.AnnounceWinner(color);
        }
    }
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}
