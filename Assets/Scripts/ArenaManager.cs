using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public Transform spawnsParent;
    public Transform charactersParent;

    public GameObject SpawnPrefab;

    public GameObject characterPrefab;

    private GameObject[] spawns;

    private void Awake()
    {
        
    }

    /// <summary>
    /// Setup a certain amount of spawns.
    /// </summary>
    /// <param name="numberOfPlayers"></param>
    public void SetupSpawns(int numberOfPlayers)
    {
        // TODO Spawn as much characters as numbers of players.

        spawns = new GameObject[numberOfPlayers];

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
        foreach(GameObject spawn in spawns)
        {
            GameObject character = Instantiate(characterPrefab, spawn.transform.position, Quaternion.identity, charactersParent);

            // Orientate to exit.
            character.transform.LookAt(Vector3.zero);
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
