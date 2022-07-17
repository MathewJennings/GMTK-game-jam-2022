using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDiceSide : MonoBehaviour {

    private GameObject diceWhoseRollCompleted;
    public GameObject player;

    private void OnEnable () {
        DiceDragManager.diceRollCompleted += registerDiceRollCompleted;
    }

    private void OnDisable () {
        DiceDragManager.diceRollCompleted -= registerDiceRollCompleted;
    }

    private void registerDiceRollCompleted (GameObject dice) {
        diceWhoseRollCompleted = dice;
    }

    void OnTriggerStay (Collider other) {
        if (other.TryGetComponent(out DiceSide sideThatIsDown)) {
            if(sideThatIsDown.transform.parent.gameObject != diceWhoseRollCompleted) {
                return;
            }
            DiceSide sideThatIsUp = sideThatIsDown.oppositeSide.GetComponent<DiceSide>();
            player.GetComponent<PlayerAttackController>().Swipe(diceWhoseRollCompleted, sideThatIsUp);
            diceWhoseRollCompleted = null;
        }
    }
}