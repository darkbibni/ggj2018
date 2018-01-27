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

        ResetGame();
    }

    public bool JoinFight(int playerIndex) {
        
        bool join = arenaMgr.AddPlayer(playerIndex);

        return join;
    }

    public bool QuitFight(int playerIndex)
    {
        bool quit = arenaMgr.RemovePlayer(playerIndex);

        return quit;
    }
    
    public bool StartFight()
    {
        if(arenaMgr.CanStartFight)
        {
            gameState = GameStates.FIGHT;

            arenaMgr.SetupSpawns();

            // TODO COROUTINE ! TRANSITION --> countdown 3 2 1 GO !
            arenaMgr.SpawnCharacters();

            return true;
        }

        return false;
    }

    public void ResetGame()
    {
        gameState = GameStates.SETUP;
        arenaMgr.ResetArena();
    }

    public void StopFight()
    {
        gameState = GameStates.END;
    }
}
