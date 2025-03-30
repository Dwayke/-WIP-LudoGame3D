using FishNet.Object;
using System.Collections.Generic;
using UnityEngine;

public class LudoMatchPlayer : NetworkBehaviour
{
    #region VARS
    public ETeam team;
    public List<BaseDisk> playerDisks;
    public bool isReady = false;
    #endregion
    #region ENGINE
    public override void OnStartClient()
    {
        base.OnStartClient();
        SetupPlayer();
        SetupDisks();
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        AnnounceGameExit();
    }
    #endregion
    #region LOCAL METHODS
    private void SetupDisks()
    {
        foreach (var disk in FindObjectsByType<BaseDisk>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if(disk.color == team)
            {
                playerDisks.Add(disk);
            }
        }
    }
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
