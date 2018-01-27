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
    public UiManager uiMgr;

    [Header("Characters")]
    public GameObject[] characterPrefabs;

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

        if(join)
        {
            uiMgr.SetPlayerReady(playerIndex, true);
        }

        return join;
    }

    public bool QuitFight(int playerIndex)
    {
        bool quit = arenaMgr.RemovePlayer(playerIndex);

        if (quit)
        {
            uiMgr.SetPlayerReady(playerIndex, false);
        }

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

            uiMgr.DisplayFightPanel(arenaMgr.PlayerCount);

            return true;
        }

        return false;
    }

    public void ResetGame()
    {
        gameState = GameStates.SETUP;
        arenaMgr.ResetArena();

        uiMgr.DisplaySetupPanel();
    }

    public void StopFight(int winnerIndex)
    {
        gameState = GameStates.END;

        uiMgr.DisplayEndPanel(winnerIndex);
    }
}
