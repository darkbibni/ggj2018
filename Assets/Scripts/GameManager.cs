using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private int numberOfPlayers = 0;

    public static GameManager singleton;

    public ArenaManager arenaMgr;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
        
        // TODO check first all players with a button "A" by example.
    }

    private void Setup()
    {
        arenaMgr.SetupSpawns(numberOfPlayers);
    }
}
