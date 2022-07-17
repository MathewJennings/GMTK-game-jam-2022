using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    public static List<Object> attackPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        attackPrefabs = new List<Object>();
        attackPrefabs.Add(Resources.Load("Sword"));
    }

    public void Attack(GameObject attackingDice, DiceSide attackingDiceSide)
    {
        DieType dieType = attackingDice.GetComponent<DieAttackProperties>().dieType;

        switch (dieType)
        {
            case DieType.SwordDie:
                SwordSwipe.Create(attackPrefabs[0], attackingDice, attackingDiceSide);
                break;
            case DieType.TeleportDie:
                Teleport.ExecuteTeleport(attackingDice, attackingDiceSide);
                break;
            default:
                throw new System.Exception("Unrecognized die type");
        }
    }
}
