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
                    RpcAssignTeam(player,ETeam.Red);
                break;
                case 2:
                    player.team = ETeam.Blue;
                    RpcAssignTeam(player, ETeam.Blue);
                break;
                case 3:
                    player.team = ETeam.Yellow;
                    RpcAssignTeam(player, ETeam.Yellow);
                break;
                case 4:
                    player.team = ETeam.Green;
                    RpcAssignTeam(player, ETeam.Green);
                break;
            default:
                break;
        }

    }
    [ObserversRpc(ExcludeOwner = true)]
    public void RpcAssignTeam(LudoMatchPlayer player, ETeam team)
    {
        player.team = team;
    }
}
