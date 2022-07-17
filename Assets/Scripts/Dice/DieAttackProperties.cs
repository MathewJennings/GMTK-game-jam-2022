using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAttackProperties : MonoBehaviour
{
    public DieType dieType;
}

public enum DieType
{
    SwordDie,
    TeleportDie
}
