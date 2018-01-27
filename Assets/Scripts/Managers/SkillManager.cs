using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public static SkillManager instance;
    
    [Header("Datas")]
    public Gun_Data gun_data;

    public Slash_Data slash_data;
    
    public Stomp_Data stomp_data;

    public Missile_Data missile_data;

    [Header("Arrays of skills")]
    // CAC - DIST
    public List<Skill> skillsA = new List<Skill>();
    public List<Skill> skillsX = new List<Skill>();

    // SHIT
    public List<Skill> skillsB = new List<Skill>();

    // ULTIMATE
    public List<Skill> skillsY = new List<Skill>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    /// <summary>
    /// Generate a copy of a list of skills depending the number of players.
    /// </summary>
    /// <param name="skillToCopy"></param>
    /// <param name="numberOfPlayers"></param>
    /// <returns></returns>
    public List<Skill> GenerateListOfSkills(List<Skill> skillToCopy, int numberOfPlayers)
    {
        List<Skill> skills = new List<Skill>(skillToCopy);

        if(skills.Count > 0)
        {
            for (int i = 0; i < 4 - numberOfPlayers; i++)
            {
                skills.RemoveAt(Random.Range(0, skills.Count));
            }
        }

        return skills;
    }
}
