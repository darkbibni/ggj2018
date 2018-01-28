using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public GameObject[] highlightVfx;
    public Color[] playerColors;

    [Header("Transfert")]
    public GameObject[] transfertPrefabs;
    public AnimationCurve curve;

    private bool isStarting;

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

    void Start()
    {
        AudioManager.singleton.PlayMusic(AudioManager.singleton.MenuMusic);
    }

    public bool JoinFight(int playerIndex) {
        
        AudioManager.singleton.PlaySFX(AudioManager.singleton.GetSFXclip("Beep"));
        bool join = arenaMgr.AddPlayer(playerIndex);

        if(join)
        {
            uiMgr.SetPlayerReady(playerIndex, true);
        }

        return join;
    }

    public bool QuitFight(int playerIndex)
    {
        AudioManager.singleton.PlayRandomize(AudioManager.singleton.GetSFXclip("Beep"));
        bool quit = arenaMgr.RemovePlayer(playerIndex);

        if (quit)
        {
            uiMgr.SetPlayerReady(playerIndex, false);
        }

        return quit;
    }
    
    public bool StartFight()
    {
        if(arenaMgr.CanStartFight && !isStarting)
        {
            isStarting = true;
            AudioManager.singleton.StopMusic();
            AudioManager.singleton.mixer.DOSetFloat("HighPass_Music", 0f, 0.8f);

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
            AudioManager.singleton.PlaySFX(AudioManager.singleton.countdown[i-1]);
            yield return new WaitForSeconds(0.33f);
        }

        AudioManager.singleton.PlayMusic(AudioManager.singleton.FightMusic);

        uiMgr.UpdateCountDown("FIGHT !");
        AudioManager.singleton.PlaySFX(AudioManager.singleton.GetSFXclip("GO"));

        yield return new WaitForSeconds(0.25f);

        uiMgr.EnableCountDown(false);

        gameState = GameStates.FIGHT;
    }

    public void ResetGame()
    {
        gameState = GameStates.SETUP;

        isStarting = false;
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
        AudioManager.singleton.StopMusic();
        AudioManager.singleton.PlaySFX(AudioManager.singleton.GetSFXclip("EndGame"));
        
        AudioManager.singleton.PlayMusic(AudioManager.singleton.MenuMusic);
        gameState = GameStates.END;

        uiMgr.DisplayEndPanel(winnerIndex);
    }
}
