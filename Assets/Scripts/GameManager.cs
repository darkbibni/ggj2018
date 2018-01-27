using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    SETUP,
    FIGHT,
    END
}

public class GameManager : MonoBehaviour {

    private int numberOfPlayers = 0;

    public static GameManager instance;

    public GameStates GameState
    {
        get { return gameState; }
    }
    private GameStates gameState;

    public ArenaManager arenaMgr;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    
    public void StartFight()
    {
        gameState = GameStates.FIGHT;
    }

    public void ResetGame()
    {
        numberOfPlayers = 0;
        gameState = GameStates.SETUP;
    }

    public void StopFight()
    {
        gameState = GameStates.END;
    }
}
