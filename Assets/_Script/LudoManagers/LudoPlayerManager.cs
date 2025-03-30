using FishNet.Object;
using System.Collections.Generic;

public class LudoPlayerManager : NetworkBehaviour
{
    public List<LudoMatchPlayer> players;
    public int readyPlayersCount = 0;

    public void AddPlayer(LudoMatchPlayer player)
    {
        players.Add(player);
    }
    public void RemovePlayer(LudoMatchPlayer player)
    {
        if(player.isReady) readyPlayersCount=-1;
        players.Remove(player);
    }
    public void AssignTeam(LudoMatchPlayer player)
    {
        switch (players.Count)   
        {
                case 1:
                    player.team = ETeam.Red;
                    CmdAssignTeam(player,ETeam.Red);
                    RpcAssignTeam(player,ETeam.Red);
                break;
                case 2:
                    player.team = ETeam.Blue;
                    CmdAssignTeam(player, ETeam.Blue);
                    RpcAssignTeam(player, ETeam.Blue);
                break;
                case 3:
                    player.team = ETeam.Yellow;
                    CmdAssignTeam(player, ETeam.Yellow);
                    RpcAssignTeam(player, ETeam.Yellow);
                break;
                case 4:
                    player.team = ETeam.Green;
                    CmdAssignTeam(player, ETeam.Green);
                    RpcAssignTeam(player, ETeam.Green);
                break;
            default:
                break;
        }

    }
    [ServerRpc(RequireOwnership =false)]
    private void CmdAssignTeam(LudoMatchPlayer player, ETeam team)
    {
        player.team = team;
    }
    [ObserversRpc(ExcludeOwner = true)]
    private void RpcAssignTeam(LudoMatchPlayer player, ETeam team)
    {
        player.team = team;
    }
}
