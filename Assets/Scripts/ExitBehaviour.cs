using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

    public ArenaManager arenaMgr;

    private void OnTriggerEnter(Collider other)
    {
        // TODO get player component.
        // other.GetComponent<>()

        // Get player and check if he has no abilities.
        if (false)
        {
            // TODO get player index.

            int winnerIndex = 0;
            arenaMgr.TriggerWin(winnerIndex);
        }
    }
}
