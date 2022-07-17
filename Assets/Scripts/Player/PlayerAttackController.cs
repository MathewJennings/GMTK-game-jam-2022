using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public static Object swordPrefab;

    // Start is called before the first frame update
    void Start()
    {
        swordPrefab = Resources.Load("Sword");
    }

    public void Swipe(GameObject attackingDice, DiceSide attackingDiceSide)
    {
        SwordSwipe.Create(swordPrefab, attackingDice, attackingDiceSide);
    }
}
