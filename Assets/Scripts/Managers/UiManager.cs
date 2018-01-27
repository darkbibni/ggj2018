using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    
    [Header("Setup UI")]
    public GameObject setupPanel;
    public Image[] playerFeedbacks;
    public Text[] textFeedbacks;

    public Sprite leaveSprite;
    public string leaveText = "to leave";

    public Sprite joinSprite;
    public string joinText = "to join";

    [Header("Fight UI")]
    public GameObject fightPanel;
    public GameObject[] playerInputs;
    public SkillsUI[] playerSkillsUI;

    // TODO actions for each player.
    // FEEDBACK the "UNO" player on UI

    [Header("End UI")]
    public GameObject endPanel;
    public Text winnerText;

    private GameObject currentPanel;

    private void Awake()
    {
    }

    public void ResetUI()
    {
        foreach(SkillsUI ui in playerSkillsUI)
        {
            ui.ResetSkillUi();
        }
    }

    public void SetPlayerReady(int playerId, bool ready)
    {
        playerFeedbacks[playerId].sprite = ready ? leaveSprite : joinSprite;
        textFeedbacks[playerId].text = ready ? leaveText : joinText;
    }

    private void SetCurrentPanel(GameObject newPanel)
    {
        if(currentPanel)
        {
            currentPanel.SetActive(false);
        }

        currentPanel = newPanel;
        if(currentPanel)
        {
            currentPanel.SetActive(true);
        }
    }

    public void DisplaySetupPanel()
    {
        // Reset panel.
        for (int i = 0; i < 4; i++)
        {
            playerFeedbacks[i].sprite = joinSprite;
            textFeedbacks[i].text = joinText;
        }

        SetCurrentPanel(setupPanel);
    }

    public void DisplayFightPanel(int playerCount)
    {
        SetCurrentPanel(fightPanel);

        for (int i = 0; i < 4; i++)
        {
            playerInputs[i].SetActive(i < playerCount ? true : false);
        }
    }

    public void DisplayEndPanel(int winnerId)
    {
        SetCurrentPanel(endPanel);

        winnerText.text = "Player " + (winnerId+1) +" wins !";
    }

    public void UseSkill(int playerId, int skillId)
    {
        playerSkillsUI[playerId].PressMainSkill(skillId);
    }

    public void FeedbackCooldown(int playerId, int skillId, float cooldown)
    {
        playerSkillsUI[playerId].TriggerCooldown(skillId, cooldown);
    }

    public void AddSkillFeedback(int playerId, int skillId)
    {

    }

    public void RemoveSkillFeedback(int playerId, int skillId)
    {

    }
}
