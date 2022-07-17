using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public static void ExecuteTeleport(GameObject attackingDie, DiceSide attackingDiceSide)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerInitialPos = player.transform.position;
        Vector3 attackingDieInitialPos = attackingDie.transform.position;

        player.transform.position = new Vector3(
            attackingDieInitialPos.x, attackingDieInitialPos.y, playerInitialPos.z);
        attackingDie.transform.position = new Vector3(
            playerInitialPos.x, playerInitialPos.y, attackingDieInitialPos.z);
    }
}
