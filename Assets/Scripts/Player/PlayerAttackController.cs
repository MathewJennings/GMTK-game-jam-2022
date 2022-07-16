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

    public void Swipe(float swipeAngle, float swipeSize, int swipeDamage)
    {
        Debug.Log("Attack angle: " + swipeAngle + " Swipe Size: " + swipeSize + " Damage: " + swipeDamage);
         
        SwordSwipe.Create(prefab, swipeAngle, swipeSize, .4f, swipeDamage, myDie, playerPosition);
    }
}
