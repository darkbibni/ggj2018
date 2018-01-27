using System;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public Transform spawnsParent;
    public Transform charactersParent;

    public GameObject SpawnPrefab;
    public GameObject characterPrefab;

    private List<int> playersIndex = new List<int>();
    public bool CanStartFight
    {
        get { return playersIndex.Count > 1; }
    }

    private GameObject[] spawns;
    
    private void Awake()
    {
        
    }

    public void ResetArena()
    {
        playersIndex.Clear();

        if(spawns != null)
        {
            foreach (GameObject spawn in spawns)
            {
                Destroy(spawn);
            }
        }
    }

    public bool AddPlayer(int playerIndex)
    {
        if(!playersIndex.Contains(playerIndex))
        {
            playersIndex.Add(playerIndex);
            return true;
        }

        return false;
    }

    public bool RemovePlayer(int playerIndex)
    {
        if (playersIndex.Contains(playerIndex))
        {
            playersIndex.Remove(playerIndex);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Setup a certain amount of spawns.
    /// </summary>
    /// <param name="numberOfPlayers"></param>
    public void SetupSpawns()
    {
        // Setup spawns
        spawns = new GameObject[playersIndex.Count];

        float turnAngle = 360f / spawns.Length;

        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i] = Instantiate(SpawnPrefab, Vector3.right * -10f, SpawnPrefab.transform.rotation, spawnsParent);
            
            spawns[i].transform.RotateAround(Vector3.zero, Vector3.up, turnAngle * i);
        }
    }
   
    /// <summary>
    /// Spawn all players.
    /// </summary>
    /// <param name="numberOfPlayers"></param>
    public void SpawnCharacters()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            GameObject character = Instantiate(characterPrefab, spawns[i].transform.position + Vector3.up * characterPrefab.transform.position.y, Quaternion.identity, charactersParent);
            
            // Orientate to exit.
            character.transform.LookAt(Vector3.zero);

            PlayerController playerInput = character.GetComponent<PlayerController>();
            playerInput.SetupPlayer(i);
        }
    }

    // Trigger win !
    public void TriggerWin(int winnerIndex)
    {
        // TODO Stop all players

        // DISPLAY WINNER.

        Debug.Log("Player " + winnerIndex + " wins !");

        GameManager.instance.StopFight();
    }
}
