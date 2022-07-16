using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwipe : MonoBehaviour
{

    private float swipeAngle;
    private float swipeSize;
    private float swipeDurationSec;
    private int damage;
    private GameObject myDie;

    private Transform initialRotation;
    private float timePercentage = 0.0f;
    //TODO add spark or something when attack lands
    //public GameObject impactEffect;

    void Start()
    {
    }

    public static SwordSwipe Create(Object prefab, float sA, float sS, float sDS, int d, GameObject mD, Transform playerPosition)
    {
        GameObject newObject = Instantiate(prefab) as GameObject;
        SwordSwipe yourObject = newObject.GetComponent<SwordSwipe>();
        yourObject.swipeAngle = sA;
        yourObject.swipeSize = sS;
        yourObject.swipeDurationSec = sDS;
        yourObject.damage = d;
        yourObject.myDie = mD;
        yourObject.transform.position = playerPosition.position;
        yourObject.transform.localScale = yourObject.transform.localScale * yourObject.swipeSize;
        CalculateInitialRotation(yourObject);
        Destroy(yourObject.gameObject, yourObject.swipeDurationSec);

        return yourObject;
    }

    private static void CalculateInitialRotation(SwordSwipe yourObject)
    {
        //rotate towards the die
        Vector3 targ = yourObject.myDie.transform.position;
        Vector3 objectPos = yourObject.transform.position;

        Vector3 relativePos = targ - objectPos;
        // rotate that vector by 90 degrees around the Z axis because LookRotation assumes y is up. I dont really get it
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relativePos;
        yourObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        //the sword sprite is at a 45 degree angle
        float spriteOffset = 45.0f;
        yourObject.transform.Rotate(new Vector3(0, 0, -spriteOffset + (yourObject.swipeAngle / 2)));
        yourObject.initialRotation = yourObject.transform;
    }

    void Update()
    {
        if (timePercentage < 1.0f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                initialRotation.rotation * Quaternion.Euler(0.0f, 0.0f, -swipeAngle),
                timePercentage);
            
            timePercentage += Time.deltaTime / swipeDurationSec;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyState>().TakeDamage(this.transform, damage);
        }
    }
}
