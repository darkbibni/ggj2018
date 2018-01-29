using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public Transform spawnsParent;
    public Transform charactersParent;

    public GameObject SpawnPrefab;
    public GameObject characterPrefab;

    public ExitBehaviour exitMgr;

    private List<int> playersIndex = new List<int>();
    public bool CanStartFight
    {
        get { return playersIndex.Count > 1; }
    }
    public int PlayerCount
    {
        get { return playersIndex.Count; }
    }

    private GameObject[] spawns;

    private GameObject[] characters;
    
    private void Awake()
    {

    }

    #region Spawn and characters

    /// <summary>
    /// Clear spawns and characters.
    /// </summary>
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

        if(characters != null)
        {
            foreach(GameObject character in characters)
            {
                Destroy(character);
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
            spawns[i].transform.localPosition += Vector3.up * 0.01f;

            spawns[i].transform.RotateAround(Vector3.zero, Vector3.up, -turnAngle * i);
        }
    }
   
    /// <summary>
    /// Spawn all players.
    /// </summary>
    /// <param name="numberOfPlayers"></param>
    public void SpawnCharacters()
    {
        characters = new GameObject[spawns.Length];

        // Generate a list of skills to distribuate to each players.
        List<Skill> skillsA = SkillManager.instance.GenerateListOfSkills(SkillManager.instance.skillsA, spawns.Length);
        List<Skill> skillsX = SkillManager.instance.GenerateListOfSkills(SkillManager.instance.skillsX, spawns.Length);
        List<Skill> skillsB = SkillManager.instance.GenerateListOfSkills(SkillManager.instance.skillsB, spawns.Length);
        List<Skill> skillsY = SkillManager.instance.GenerateListOfSkills(SkillManager.instance.skillsY, spawns.Length);

        playersIndex.Sort();

        for (int i = 0; i < spawns.Length; i++)
        {
            characters[i] = Instantiate(characterPrefab, spawns[i].transform.position + Vector3.up * characterPrefab.transform.position.y, Quaternion.identity, charactersParent);

            // Orientate to exit.
            characters[i].transform.LookAt(Vector3.zero);

            PlayerController playerInput = characters[i].GetComponent<PlayerController>();

            // Sort player by controllers.
            playerInput.SetupPlayer(playersIndex[i]);

            // Add 4 skills to the character
            {
                if (skillsA.Count > 0)
                {
                    playerInput.AddSkill(skillsA[0]);
                    skillsA.RemoveAt(0);
                }

                if (skillsX.Count > 0)
                {
                    playerInput.AddSkill(skillsX[0]);
                    skillsX.RemoveAt(0);
                }

                if (skillsB.Count > 0)
                {
                    playerInput.AddSkill(skillsB[0]);
                    skillsB.RemoveAt(0);
                }

                if (skillsY.Count > 0)
                {
                    playerInput.AddSkill(skillsY[0]);
                    skillsY.RemoveAt(0);
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="winnerIndex"></param>
    public void TriggerWin(int winnerIndex)
    {
        GameManager.instance.StopFight(winnerIndex);
    }
}
