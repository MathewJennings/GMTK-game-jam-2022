using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public static List<Object> attackPrefabs;
    public AudioClip swipeSound;
    public AudioClip warpSound;

    // Start is called before the first frame update
    void Start()
    {
        attackPrefabs = new List<Object>();
        attackPrefabs.Add(Resources.Load("Sword"));
    }

    public void Attack(GameObject attackingDice, DiceSide attackingDiceSide)
    {
        DiceSide.Ability ability = attackingDiceSide.ability;
        switch (ability)
        {
            case DiceSide.Ability.None:
                break;
            case DiceSide.Ability.SwordSwipe:
                SwordSwipe.Create(attackPrefabs[0], attackingDice, attackingDiceSide, swipeSound);
                break;
            case DiceSide.Ability.Teleport:
                Teleport.ExecuteTeleport(attackingDice, attackingDiceSide, warpSound);
                break;
            default:
                throw new System.Exception("Unrecognized die type");
        }
    }
}
