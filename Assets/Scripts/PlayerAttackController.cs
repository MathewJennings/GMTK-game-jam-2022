using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public Transform playerPosition;
    public GameObject myDie;
    public static Object prefab;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("Sword");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Swipe()
    {
        // These would be the properties from the die
        var swipeAngle = Random.Range(60.0f, 180.0f);
        var swipeSize = Random.Range(.5f, 2.0f);
        var swipeDamage = 1;

        Debug.Log("Attack angle: " + swipeAngle + " Swipe Size: " + swipeSize);
        SwordSwipe.Create(prefab, swipeAngle, swipeSize, .4f, swipeDamage, myDie, playerPosition);
    }
}
