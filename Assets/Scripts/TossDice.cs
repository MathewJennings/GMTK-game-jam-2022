using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TossDice : MonoBehaviour {

    Rigidbody rigidbody;
    public static Vector3 diceVelocity;

    void Start () {
        rigidbody = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update () {
        diceVelocity = rigidbody.velocity;
    }

    public void OnRollDice(InputValue value) {
        //Debug.Log ("Rolling Dice");
        float dirX = Random.Range (0, 500);
        float dirY = Random.Range (0, 500);
        float dirZ = Random.Range (0, 500);
        // transform.position = new Vector3 (0, 2, 0);
        transform.Translate(Vector3.back);
        transform.rotation = Quaternion.identity;
        rigidbody.AddForce (transform.forward * -500);
        rigidbody.AddTorque (dirX, dirY, dirZ);
    }
}