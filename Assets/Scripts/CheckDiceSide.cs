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
        if (other.gameObject.transform.parent.gameObject != diceWhoseRollCompleted) {
            return;
        }
        GameObject sideThatIsUp = other.GetComponent<DiceSide>().oppositeSide;
        Debug.Log ("Rolled " + sideThatIsUp);
        // These would be the properties from the die
        var swipeAngle = Random.Range(60.0f, 180.0f);
        var swipeSize = Random.Range(.5f, 2.0f);
        var swipeDamage = 1;

        player.GetComponent<PlayerAttackController>().Swipe(swipeAngle, swipeSize, swipeDamage);
        diceWhoseRollCompleted = null;
    }
}