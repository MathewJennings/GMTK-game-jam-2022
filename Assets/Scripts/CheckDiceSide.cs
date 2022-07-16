using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDiceSide : MonoBehaviour {

    private GameObject diceWhoseRollCompleted;

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
        if (other.gameObject.transform.parent.gameObject != diceWhoseRollCompleted) {
            return;
        }
        GameObject sideThatIsUp = other.GetComponent<DiceSide>().oppositeSide;
        Debug.Log ("Rolled " + sideThatIsUp);
        diceWhoseRollCompleted = null;
    }
}