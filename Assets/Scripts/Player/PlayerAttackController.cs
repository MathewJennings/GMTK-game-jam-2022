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
        DieType dieType = attackingDice.GetComponent<DieAttackProperties>().dieType;

        switch (dieType)
        {
            case DieType.SwordDie:
                SwordSwipe.Create(attackPrefabs[0], attackingDice, attackingDiceSide);
                GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(swipeSound, 0.5f);
                break;
            case DieType.TeleportDie:
                Teleport.ExecuteTeleport(attackingDice, attackingDiceSide);
                GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().PlayOneShot(warpSound, 0.5f);
                break;
            default:
                throw new System.Exception("Unrecognized die type");
        }
    }
}
