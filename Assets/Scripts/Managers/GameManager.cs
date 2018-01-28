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
            StartCoroutine(CountDown());

            arenaMgr.SetupSpawns();
            
            arenaMgr.SpawnCharacters();

            uiMgr.DisplayFightPanel(arenaMgr.PlayerCount);

            return true;
        }

        return false;
    }

    private IEnumerator CountDown()
    {
        uiMgr.EnableCountDown(true);

        for (int i = 3; i > 0; i--)
        {
            uiMgr.UpdateCountDown(i.ToString());
            yield return new WaitForSeconds(0.33f);
        }

        uiMgr.UpdateCountDown("FIGHT !");

        yield return new WaitForSeconds(0.25f);

        uiMgr.EnableCountDown(false);

        gameState = GameStates.FIGHT;
    }

    public void ResetGame()
    {
        gameState = GameStates.SETUP;
        arenaMgr.ResetArena();

        uiMgr.ResetUI();
        uiMgr.DisplaySetupPanel();

        foreach(GameObject highlight in arenaMgr.exitMgr.highlights)
        {
            highlight.SetActive(false);
        }
    }

    public void StopFight(int winnerIndex)
    {
        gameState = GameStates.END;

        uiMgr.DisplayEndPanel(winnerIndex);
    }
}
