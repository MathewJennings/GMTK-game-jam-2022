using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDiceSide : MonoBehaviour {
    Vector3 diceVelocity;

    void Start () {
        diceVelocity = TossDice.diceVelocity;
    }

    void OnTriggerStay (Collider other) {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f) {
            switch (other.gameObject.name) {
                // NOTE: the case values are the sides that are DOWN. The logged values are the sides that are UP.
                case "Side 1":
                    //Debug.Log ("Rolled a 6");
                    break;
                case "Side 2":
                    //Debug.Log ("Rolled a 5");
                    break;
                case "Side 3":
                    //Debug.Log ("Rolled a 4");
                    break;
                case "Side 4":
                    //Debug.Log ("Rolled a 3");
                    break;
                case "Side 5":
                    //Debug.Log ("Rolled a 2");
                    break;
                case "Side 6":
                    //Debug.Log ("Rolled a 1");
                    break;
            }
        }
    }
}