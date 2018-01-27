using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

    public ArenaManager arenaMgr;

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
}
