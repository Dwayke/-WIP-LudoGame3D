using UnityEngine;

public class LudoCommonTypes : MonoBehaviour
{
    #region VARS
    #endregion
    #region ENGINE
    #endregion
    #region MEMBER METHODS
    #endregion
    #region LOCAL METHODS
    #endregion
}

public enum ETeam
{
    Red = 0, Blue = 1, Yellow = 2, Green = 3
}
public enum ETileColor
{
    Red = 0, Green = 1, Blue = 2, Yellow = 3
}
public enum ETileType
{
    Base = 0, Waypoint = 1, Home = 2
}
public enum EPieceState
{
    Locked = 0, Free = 1, Home = 2
}
public enum EGameState
{
    FirstPlayerSelection = 0, DiceRoll = 1, PieceSelection = 2, PieceMotion = 3, NextPlayerDetermination = 4, GameEnd = 5
}
