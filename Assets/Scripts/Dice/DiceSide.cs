using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour {
    [SerializeField]
    public GameObject oppositeSide;
    public float swipeAngleDegrees;
    public float swipeScale;
    public float swipeTimeInSeconds;
    public int swipeDamage;
    public Ability ability;

    public enum Ability
    {
        None,
        SwordSwipe,
        Teleport
    }
}
