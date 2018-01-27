using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

/// <summary>
/// Manage inputs not in fight.
/// </summary>
public class PlayersInputManager : MonoBehaviour {

    // TODO Feedback UI !!
    public GameObject ui;

    private void Update()
    {
        switch(GameManager.instance.GameState)
        {
            case GameStates.SETUP: HandleFightSetup(); break;
            case GameStates.END: HandleFightEnd(); break;
        }
    }

    private void HandleFightSetup()
    {
        for (int i = 0; i < ReInput.players.allPlayerCount; i++)
        {
            Player p = ReInput.players.AllPlayers[i];

            HandleFightSetupForPlayer(p);
        }
    }

    private void HandleFightSetupForPlayer(Player p)
    {
        if (p.GetButtonDown("A"))
        {
            if(GameManager.instance.JoinFight(p.id))
            {
                Debug.Log(p.id + " joins the fight !");
            }
        }

        if (p.GetButtonDown("B"))
        {
            if(GameManager.instance.QuitFight(p.id))
            {
                Debug.Log(p.id + " leaves the fight !");
            }
        }
        
        if(p.GetButtonDown("Pause"))
        {
            if (GameManager.instance.StartFight())
            {
                Debug.Log("START !");
            }
        }
    }

    private void HandleFightEnd()
    {
        for (int i = 0; i < ReInput.players.allPlayerCount; i++)
        {
            Player p = ReInput.players.AllPlayers[i];
        }
    }
}
