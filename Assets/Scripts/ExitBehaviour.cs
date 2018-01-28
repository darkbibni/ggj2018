using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

    public ArenaManager arenaMgr;

    public GameObject[] highlights;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        // Get player and check if he has no abilities.
        if (player)
        {
            if(player.IsEmpty)
            {
                arenaMgr.TriggerWin(player.playerId);
            }
        }
    }

    public void HighlightExit(int playerId, bool highlighted)
    {
        highlights[playerId].SetActive(highlighted);
    }
}
