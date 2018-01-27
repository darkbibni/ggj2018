using System.Collections;
using System.Collections.Generic;
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
    // TODO actions for each player.
    // FEEDBACK the "UNO" player on UI

    [Header("End UI")]
    public GameObject endPanel;

    private GameObject currentPanel;

    private void Awake()
    {

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

    public void DisplayFightPanel()
    {
        SetCurrentPanel(null);
    }
}
