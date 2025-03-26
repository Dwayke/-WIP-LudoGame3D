using FishNet.Object;

public class LudoMatchPlayer : NetworkBehaviour
{
    #region VARS
    public ETeam team;
    public bool isReady = false;
    #endregion
    #region ENGINE
    public override void OnStartClient()
    {
        base.OnStartClient();
        SetupPlayer();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        AnnounceGameExit();
    }
    #endregion
    #region LOCAL METHODS
    private void SetupPlayer()
    {
        CmdAddPlayer();
    }
    private void AnnounceGameExit()
    {
        CmdRemovePlayer();
        //Announce Game Exit
    }
    [ServerRpc(RequireOwnership = true)]
    private void CmdAddPlayer()
    {
        LudoManagers.Instance.PlayerManager.AddPlayer(this);
        LudoManagers.Instance.PlayerManager.AssignTeam(this);

    }
    [ServerRpc(RequireOwnership = true)]
    private void CmdRemovePlayer()
    {
        LudoManagers.Instance.PlayerManager.RemovePlayer(this);
    }
    #endregion
}
