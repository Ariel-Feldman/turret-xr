using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState GameState = GameState.Playing;


}


public enum GameState
{
    Playing,
    Pause
}